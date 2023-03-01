using MediatR;
using Newtonsoft.Json;
using SeedWork;
using Taaldc.Sales.API.Application.Commands;
using Taaldc.Sales.Api.Application.Commands.SellUnit;
using Taaldc.Sales.Api.Application.Queries.Buyers;
using Taaldc.Sales.Api.Application.Queries.Orders;
using Taaldc.Sales.Api.Application.Queries.UnitReplicas;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;
using Taaldc.Sales.Domain.Events;
using Taaldc.Sales.Domain.Exceptions;

namespace Taaldc.Sales.Api.Application.Commands.SellUnit;

public class SellUnitCommandHandler : IRequestHandler<SellUnitCommand, CommandResult>
{
    private readonly IBuyerQueries _buyerQueries;
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitQueries _unitQueries;
    
    private readonly IAmCurrentUser _currentUser;
    private readonly ILogger<SellUnitCommandHandler> _logger;
    private readonly IMediator _mediator;

    private readonly IOrderRepository _salesRepository;

    public SellUnitCommandHandler(IBuyerQueries buyerQueries, 
        IOrderRepository orderRepository, 
        IUnitQueries unitQueries, 
        IAmCurrentUser currentUser, 
        ILogger<SellUnitCommandHandler> logger, 
        IMediator mediator, 
        IOrderRepository salesRepository)
    {
        _buyerQueries = buyerQueries;
        _orderRepository = orderRepository;
        _unitQueries = unitQueries;
        _currentUser = currentUser;
        _logger = logger;
        _mediator = mediator;
        _salesRepository = salesRepository;
    }

    public async Task<CommandResult> Handle(SellUnitCommand request, CancellationToken cancellationToken)
    {
        
        try
        {
            var buyerExists = await _buyerQueries.CheckIfBuyerExists(request.BuyerId);

            if (!buyerExists)
                throw new SalesDomainException(nameof(SellUnitCommandHandler), new Exception("Buyer not found."));
            
            var unitAvailabity = await ValidateAndCheckUnitsAvailability(request);

            //create order
            var order = _salesRepository.CreateOrder(
                request.BuyerId, 
                request.Broker, 
                request.TransactionDate, 
                request.Discount,
                request.Remarks);
            
            //nevermind the overwrite
            order.AddBrokerDetail(_currentUser.Email, _currentUser.GetCompany(), _currentUser.GetPrcLicense(), _currentUser.Name);
           

            //add order item in order object
            foreach (var item in request.OrderItems)
            {
                order.AddOrUpdateOrderItem(item.UnitId, item.Price, null);
            }

            //if RF should had been paid
            if (request.ReservationFee > 0)
            {
                if (request.ReservationConfirmation == string.Empty)
                    throw new SalesDomainException(nameof(SellUnitCommand),
                        new ArgumentNullException(
                            $"{nameof(SellUnitCommand.ReservationConfirmation)} must not be empty or null."));

                // Add payment for reservation
                order.AddPayment(
                    PaymentType.GetId(PaymentType.Reservation),
                    TransactionType.GetTypeId(TransactionType.ForReservation),
                    order.TransactionDate,
                    request.ReservationConfirmation,
                    request.PaymentMethod,
                    request.ReservationFee,
                    request.Remarks);
            }

            //if DP has been paid
            if (request.Downpayment > 0)
            {
                //if paid, require this
                if (request.DownpaymentConfirmation == string.Empty)
                    throw new ArgumentNullException(nameof(SellUnitCommand));

                //add payment fore acquisition
                order.AddPayment(
                    PaymentType.GetId(PaymentType.PartialDownPayment),
                    TransactionType.GetTypeId(TransactionType.ForAcquisition),
                   order.TransactionDate,
                    request.DownpaymentConfirmation,
                    request.PaymentMethod,
                    request.Downpayment,
                    request.Remarks);
            }

            //add order item in order object
            foreach (var item in request.OrderItems)
            {
                order.AddDomainEvent(new UnitReplicaStatusChangedToReservedDomainEvent(item.UnitId, 3, "RESERVED"));
            }

      
          

            return CommandResult.Success(order.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(nameof(SellUnitCommandHandler), JsonConvert.SerializeObject(ex));
            return CommandResult.Failed(null, ex.InnerException.Message);
        }
    }

    private async Task<IEnumerable<UnitAvailability>> ValidateAndCheckUnitsAvailability(SellUnitCommand request)
    {
        //check for duplicate unit ids on the request
        if (request.OrderItems.Count() > 1 && request.OrderItems.Select(i => i.UnitId).Distinct().Count() != request.OrderItems.Count())
        {
            throw new SalesDomainException(nameof(SellUnitCommandHandler),
                new Exception("Possible duplicate unit id in the request."));
        }

        //get unit availability of the requested unit ids
        var unitAvailabity =
            await _unitQueries.GetUnitAvailabilityAsync(request.OrderItems.Select(i => i.UnitId).ToArray());

        //check if NOT EVERYTHING is available -- even if just one is unavailable
        if (!unitAvailabity.All(i => i.IsAvailable))
        {
            throw new SalesDomainException(nameof(SellUnitCommandHandler),
                new Exception("A unit or more is not anymore available"));
        }

        //check if NOT EVERYTHING is a Parking Unit -- must have at least 1 RESIDENTIAL (NON-PARKING)
        if (unitAvailabity.All(i => new[] { 6, 7, 8 }.Contains(i.UnitTypeId)))
        {
            throw new SalesDomainException(nameof(SellUnitCommandHandler),
                new Exception("A residential (non-park) unit is required."));
        }

        return unitAvailabity;
    }
}