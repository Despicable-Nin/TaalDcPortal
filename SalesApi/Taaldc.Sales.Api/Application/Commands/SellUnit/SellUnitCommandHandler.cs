using MediatR;
using Newtonsoft.Json;
using SeedWork;
using Taaldc.Sales.Api.Application.Commands.SellUnit;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.API.Application.Commands.SellUnit;

public class SellUnitCommandHandler : IRequestHandler<SellUnitCommand, SellUnitCommandResult>
{
    private readonly IBuyerRepository _buyerRepository;
    private readonly IAmCurrentUser _currentUser;
    private readonly ILogger<SellUnitCommandHandler> _logger;
    private readonly IMediator _mediator;

    private readonly IOrderRepository _salesRepository;

    public SellUnitCommandHandler(
        IOrderRepository salesRepository,
        IBuyerRepository buyerRepository,
        ILogger<SellUnitCommandHandler> logger,
        IAmCurrentUser currentUser,
        IMediator mediator)
    {
        _salesRepository = salesRepository;
        _buyerRepository = buyerRepository;
        _logger = logger;
        _currentUser = currentUser;
        _mediator = mediator;
    }

    public async Task<SellUnitCommandResult> Handle(SellUnitCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var buyer = _buyerRepository.GetByEmail(request.EmailAddress);

            var buyerId = buyer?.Id;

            //upsert buyer
            buyer = _buyerRepository.Upsert(
                request.Salutation,
                request.FirstName,
                "",
                request.LastName,
                new DateTime(1900,1,1),
                1,
                request.EmailAddress,
                request.ContactNo,
                "",
                buyerId);

            //persist to database
            await _buyerRepository.UnitOfWork.SaveChangesAsync();

            //add order
            var sale = _salesRepository.AddOrder(request.UnitId,
                TransactionType.GetTypeId(TransactionType.ForReservation),
                buyer.Id,
                request.Code,
                request.Broker,
                request.Remarks,
                request.SellingPrice);

            //if RF has been paid
            if (request.Reservation > 0)
            {
                if (request.ReservationConfirmNo == string.Empty)
                    throw new ArgumentNullException(nameof(SellUnitCommand));


                // Add payment for reservation
                sale.AddPayment(
                    PaymentType.GetId(PaymentType.Reservation),
                    TransactionType.GetTypeId(TransactionType.ForReservation),
                    request.PaymentDate,
                    request.ReservationConfirmNo,
                    request.PaymentMethod,
                    request.Reservation,
                    request.Remarks);
            }

            //if DP has been paid
            if (request.DownPayment > 0)
            {
                //if paid, require this
                if (request.DownPaymentConfirmNo == string.Empty)
                    throw new ArgumentNullException(nameof(SellUnitCommand));

                var correlationId = request.DownPaymentConfirmNo == request.ReservationConfirmNo
                    ? request.DownPaymentConfirmNo
                    : string.Empty;


                //add payment fore acquisition
                sale.AddPayment(
                    PaymentType.GetId(PaymentType.PartialDownPayment),
                    TransactionType.GetTypeId(TransactionType.ForAcquisition),
                    request.PaymentDate,
                    request.DownPaymentConfirmNo,
                    request.PaymentMethod,
                    request.Reservation,
                    request.Remarks,
                    request.ReservationConfirmNo == request.DownPaymentConfirmNo
                        ? request.ReservationConfirmNo
                        : correlationId);
            }


            await _salesRepository.UnitOfWork.SaveChangesAsync();

            await _mediator.Publish(new UpdateUnitReplicaStatusNotif(request.UnitId, 3, "RESERVED"));

            return SellUnitCommandResult.Create(true, "", new Dictionary<string, object>
            {
                { "UnitId", sale.GetUnitId }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(nameof(SellUnitCommandHandler), JsonConvert.SerializeObject(ex));
            return SellUnitCommandResult.Create(false, ex.InnerException.Message, new Dictionary<string, object>());
        }
    }
}