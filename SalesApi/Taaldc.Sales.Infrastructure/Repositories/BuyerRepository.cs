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
       return  _context.Buyers.AsNoTracking().SingleOrDefault(i => i.Id == id);
    }

    public Buyer Upsert(string salutation, string firstName, string lastName, string emailAddress, string contactNo,
        string country, string province, string townCity, string zipCode, int? buyerId)
    {
        Buyer buyer = default;
        if (buyerId.HasValue)
        {
            //update
            buyer = _context.Buyers.Find(buyerId.Value);
            buyer.UpdateName(salutation, firstName,lastName);
            buyer.UpdateDetails(emailAddress, contactNo, country, province, townCity,
                zipCode);

            return _context.Buyers.Update(buyer).Entity;
        }

        buyer = new(salutation, firstName, lastName, emailAddress, contactNo, country, province, townCity, zipCode);
        return _context.Buyers.Add(buyer).Entity;
    }
}