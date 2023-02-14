using AutoMapper;
using Taaldc.Sales.API.Application.Commands.AddPayment;
using Taaldc.Sales.API.Application.Commands.SellUnit;
using Taaldc.Sales.Api.DTO;

namespace Taaldc.Sales.Api.Mappers;

public class MyProfile : Profile
{
    public MyProfile()
    {
        // CreateMap<SellUnitDTO, SellUnitCommand>()
        //     .ConstructUsing(o => new SellUnitCommand(o.Code, o.Broker, o.IsRefundable, o.UnitId, o.SellingPrice,
        //         o.Salutation, o.FirstName, o.LastName, o.EmailAddress, o.ContactNo, o.Address, o.Country, o.Province,
        //         o.TownCity,
        //         o.ZipCode, o.Reservation, o.ReservationConfirmNo, o.DownPayment, o.DownpaymentConfirmNo, o.PaymentDate,
        //         o.PaymentMethod, o.Remarks));

        CreateMap<AddPaymentDTO, AddPaymentCommand>()
            .ConstructUsing(o => new AddPaymentCommand(o.TransactionId, o.PaymentMethod, o.AmountPaid, o.Remarks,
                o.ConfirmationNumber, o.TransactionTypeId, o.PaymentTypeId, o.PaymentDate, o.CorrelationId));
    }
}