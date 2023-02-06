using System.Runtime.Serialization;
using MediatR;

namespace Taaldc.Sales.API.Application.Commands.SellUnit;

public class SellUnitCommand : IRequest<SellUnitCommandResult>
{
    public SellUnitCommand(
        string code, 
        string broker,
        bool isRefundable, 
        int unitId, 
        decimal sellingPrice,
        string salutation, 
        string firstName, 
        string lastName, 
        string emailAddress, 
        string contactNo, 
        string address,
        string country,
        string province, 
        string townCity, 
        string zipCode, 
        decimal reservation,
        string reservationConfirmNo, 
        decimal downPayment, 
        string downPaymentConfirmNo, 
        DateTime paymentDate,
        string paymentMethod, 
        string remarks)
    {
        Code = code;
        Broker = broker?? "In-house";
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

    public string Code { get; private set; }
 
    public string Broker { get;private set; }
  
    public bool IsRefundable { get;private set; }

    public int UnitId { get;private set; }
   
    public decimal SellingPrice { get;private set; }

    public string Salutation { get;private  set; }
  
    public string FirstName { get;private  set; }
    
    public string LastName { get;private  set; }
   
    public string EmailAddress { get;private  set; }

    public string ContactNo { get;private  set; }
   
    public string Address { get; private set; }
    public string Country { get;private  set; }
   
    public string Province { get;private  set; }
   
    public string TownCity { get;private  set; }
    
    public string ZipCode { get;private set; }
    
    public decimal Reservation { get;private set; }
    public string ReservationConfirmNo { get; private set; }
    
    public decimal DownPayment { get;private set; } 
    public string DownPaymentConfirmNo { get; private set; }
  
    public DateTime PaymentDate { get;private set; }
    
    public string PaymentMethod { get;private set; }
    
    public string Remarks { get; private set; }

}