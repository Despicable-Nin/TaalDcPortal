using FluentValidation;
using MediatR;
using SeedWork;
using Taaldc.Sales.Api.Application.Commands.SellUnit;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;
using Taaldc.Sales.Domain.Exceptions;

namespace Taaldc.Sales.API.Application.Commands.SellUnit;

public class SellUnitCommandHandler : IRequestHandler<SellUnitCommand, SellUnitCommandResult>
{
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

    private readonly IOrderRepository _salesRepository;
    private readonly IBuyerRepository _buyerRepository;
    private readonly ILogger<SellUnitCommandHandler> _logger;
    private readonly IAmCurrentUser _currentUser;
	private readonly IMediator _mediator;

	public async Task<SellUnitCommandResult> Handle(SellUnitCommand request, CancellationToken cancellationToken)
    {
        Buyer buyer = _buyerRepository.GetByEmail(request.EmailAddress);
        
        var buyerId = buyer?.Id;

        buyer = _buyerRepository.Upsert(request.Salutation, request.FirstName, request.LastName,
            request.EmailAddress,
            request.ContactNo, request.Address, request.Country, request.Province, request.TownCity, request.ZipCode, buyerId);

        await _buyerRepository.UnitOfWork.SaveChangesAsync();

        if (request.ReservationConfirmNo == string.Empty) throw new ArgumentNullException(nameof(SellUnitCommand));

        var sale = _salesRepository.AddOrder(request.UnitId,
            TransactionType.GetTypeId(TransactionType.ForReservation),
            buyer.Id,
            request.Code,
            request.Broker,
            request.Remarks,
            request.SellingPrice);

        // var reservation = sale.Payments
        //     .FirstOrDefault(i => i.GetTransactionTypeId() == TransactionType.GetTypeId(TransactionType.ForReservation));
        //
        // if (reservation != default)
        //     throw new SalesDomainException(nameof(SellUnitCommandHandler),
        //         new ValidationException("Reservation already exists."));

        if (request.Reservation != default)
        {
            
            // Add payment for reservation
            sale.AddPayment(
                PaymentType.GetId(PaymentType.Reservation),
                TransactionType.GetTypeId(TransactionType.ForReservation),
                request.PaymentDate,
                request.ReservationConfirmNo,
                request.PaymentMethod,
                request.Reservation,
                request.Remarks,
                default);
            
           
        }
        
        
        if (request.DownPayment != default)
        {
            // var downpayment = sale.Payments
            //     .FirstOrDefault(i => i.GetTransactionTypeId() == TransactionType.GetTypeId(TransactionType.ForAcquisition));
            //
            // if (downpayment != default)
            //     throw new SalesDomainException(nameof(SellUnitCommandHandler),
            //         new ValidationException("Downpayment already exists."));
            
            //add payment fore acquisition
            sale.AddPayment(
                PaymentType.GetId(PaymentType.PartialDownPayment),
                TransactionType.GetTypeId(TransactionType.ForAcquisition),
                request.PaymentDate,
                request.DownPaymentConfirmNo,
                request.PaymentMethod,
                request.Reservation,
                request.Remarks,
                request.ReservationConfirmNo == request.DownPaymentConfirmNo ? request.ReservationConfirmNo : default);
        }

        try { 
            await _salesRepository.UnitOfWork.SaveChangesAsync(default);

            await _mediator.Publish(new UpdateUnitReplicaStatusNotif(request.UnitId, 3, "RESERVED"));

            return SellUnitCommandResult.Create(true, "", new Dictionary<string, object>()
		    {
			    { "UnitId", sale.GetUnitId },

		    });
		}
		catch(Exception ex) {
            return SellUnitCommandResult.Create(false, ex.Message, new Dictionary<string, object>());
        }

		
    }
}