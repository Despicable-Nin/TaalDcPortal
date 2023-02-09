using MediatR;

namespace Taaldc.Sales.API.Application.Commands.SellUnit;

public class SellUnitCommand : IRequest<SellUnitCommandResult>
{
    public SellUnitCommand(string code, string broker, bool isRefundable, int unitId, decimal sellingPrice,
        string salutation, string firstName, string lastName, string emailAddress, string contactNo, string address,
        string country, string province, string townCity, string zipCode, decimal reservation,
        string reservationConfirmNo, decimal downPayment, string downPaymentConfirmNo, DateTime paymentDate,
        string paymentMethod, string remarks)
    {
        Code = code;
        Broker = broker ?? "In-house";
        IsRefundable = isRefundable;
        UnitId = unitId;
        SellingPrice = sellingPrice;
        Salutation = salutation;
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        ContactNo = contactNo;
        Address = address;
        Country = country;
        Province = province;
        TownCity = townCity;
        ZipCode = zipCode;
        Reservation = reservation;
        ReservationConfirmNo = reservationConfirmNo;
        DownPayment = downPayment;
        DownPaymentConfirmNo = downPaymentConfirmNo;
        PaymentDate = paymentDate;
        PaymentMethod = paymentMethod;
        Remarks = remarks;
    }

    public string Code { get; }

    public string Broker { get; }

    public bool IsRefundable { get; }

    public int UnitId { get; }

    public decimal SellingPrice { get; }

    public string Salutation { get; }

    public string FirstName { get; }

    public string LastName { get; }

    public string EmailAddress { get; }

    public string ContactNo { get; }

    public string Address { get; }
    public string Country { get; }

    public string Province { get; }

    public string TownCity { get; }

    public string ZipCode { get; }

    public decimal Reservation { get; }
    public string ReservationConfirmNo { get; }

    public decimal DownPayment { get; }
    public string DownPaymentConfirmNo { get; }

    public DateTime PaymentDate { get; }

    public string PaymentMethod { get; }

    public string Remarks { get; }
}