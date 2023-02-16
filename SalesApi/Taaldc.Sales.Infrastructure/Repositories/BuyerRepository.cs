using System.Diagnostics.Metrics;
using System.IO;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using SeedWork;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;
using Taaldc.Sales.Domain.Exceptions;

namespace Taaldc.Sales.Infrastructure.Repositories;

public class BuyerRepository : IBuyerRepository
{
    private readonly SalesDbContext _context;

    public BuyerRepository(SalesDbContext context)
    {
        _context = context ?? throw new SalesDomainException(nameof(BuyerRepository),
            new ArgumentNullException($"{nameof(context)} should not be null."));
    }

    public IUnitOfWork UnitOfWork => _context;


    public Buyer GetByEmail(string email)
    {
        return _context.Buyers.AsNoTracking().SingleOrDefault(i => i.EmailAddress == email);
    }

    public Buyer GetById(int id)
    {
        return _context.Buyers.AsNoTracking().SingleOrDefault(i => i.Id == id);
    }

    public Buyer UpdateAddress(int buyerId, AddressTypeEnum type, string street, string city, string state, string country, string zipCode)
    {
        if(buyerId > 0)
        {
            Buyer buyer = _context.Buyers.Find(buyerId);

            if (buyer == null) throw new ArgumentNullException(nameof(Buyer));

            var address = new Address(type, street, city, state, country, zipCode);
            
            //TODO: Refactor
            //
            // switch (type)
            // {
            //     case AddressTypeEnum.Home:
            //         buyer.HomeAddress = address;
            //         break;
            //     case AddressTypeEnum.Business:
            //         buyer.BusinessAddress = address;
            //         break;
            //     case AddressTypeEnum.Billing:
            //         buyer.BillingAddress = address;
            //         break;
            //
            // }

            return _context.Buyers.Update(buyer).Entity;
        }

        throw new ArgumentNullException(buyerId.ToString());
    }

    public Buyer UpdateCompany(int buyerId, string name, string address, string industry, string phoneNo, string mobileNo, string faxNo, string emailAddress, string tin, string secRegNo, string president, string corpSec)
    {
        if (buyerId > 0)
        {
            Buyer buyer = _context.Buyers.Find(buyerId);

            if (buyer == null) throw new ArgumentNullException(nameof(Buyer));

            buyer.Company = new Company(name, address, industry, phoneNo, mobileNo, faxNo, emailAddress, tin, secRegNo, president, corpSec);

            return _context.Buyers.Update(buyer).Entity;
        }

        throw new ArgumentNullException(buyerId.ToString());
    }

    public Buyer UpdateIDInformation(int buyerId, string occupation, string tin, string govIssuedID, DateTime govIssuedIDValidUntil)
    {
        if(buyerId > 0)
        {
            Buyer buyer = _context.Buyers.Find(buyerId);
            buyer.UpdateIDInformation(occupation, tin, govIssuedID, govIssuedIDValidUntil);
            return _context.Buyers.Update(buyer).Entity;
        }

        throw new ArgumentNullException(buyerId.ToString());
    }

    public Buyer Upsert(string salutation,
        string firstName,
        string middleName,
        string lastName,
        DateTime doB,
        int civilStatusId,
        string emailAddress,
        string phoneNo,
        string mobileNo,
        int? buyerId)
    {
        Buyer buyer = default;

        if (buyerId.HasValue)
        {
            //update
            buyer = _context.Buyers.Find(buyerId.Value);
            buyer.UpdateBuyer(salutation, firstName, middleName, lastName, doB, civilStatusId);
            buyer.UpdateContactDetails(emailAddress, phoneNo, mobileNo);

            return _context.Buyers.Update(buyer).Entity;
        }

        buyer = new Buyer(salutation, firstName, middleName, lastName, doB, civilStatusId);
        buyer.UpdateContactDetails(emailAddress, phoneNo, mobileNo);

        return _context.Buyers.Add(buyer).Entity;
    }
}