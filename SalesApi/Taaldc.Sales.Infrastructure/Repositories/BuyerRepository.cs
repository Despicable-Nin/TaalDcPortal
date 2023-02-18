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


    /// <summary>
    /// Only to be used to fetch then update entity.
    /// Never use AsNoTracking if the fetched entity will be updated.
    /// </summary>
    /// <param name="email">Unique email address of the Buyer entity</param>
    /// <returns></returns>
    public async Task<Buyer> GetByEmailAsync(string email)
    {
        return await _context.Buyers.SingleOrDefaultAsync(i => i.EmailAddress == email);
    }

    /// <summary>
    /// Only to be used to fetch then update entity.
    ///  Never use AsNoTracking if the fetched entity will be updated.
    /// </summary>
    /// <param name="id">Id (PK) Of Buyer entity</param>
    /// <returns></returns>
    public async Task<Buyer> GetByIdAsync(int id)
    {
        return await _context.Buyers.SingleOrDefaultAsync(i => i.Id == id);
    }

    /// <summary>
    /// Add or update a Buyer entity
    /// </summary>
    /// <param name="buyer"></param>
    /// <returns></returns>
    public Buyer Upsert(Buyer buyer)
    {
        if (buyer.IsTransient())
        {
            return _context.Buyers.Add(buyer).Entity;
        }

        return _context.Buyers.Update(buyer).Entity;
    }
}