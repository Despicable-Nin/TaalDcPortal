using Microsoft.EntityFrameworkCore;
using SeedWork;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;
using Taaldc.Sales.Domain.Exceptions;

namespace Taaldc.Sales.Infrastructure.Repositories;

public class UnitReplicaRepository : IUnitReplicaRepository
{
    private readonly SalesDbContext _context;

    public UnitReplicaRepository(SalesDbContext context)
    {
        _context = context ?? throw new SalesDomainException(nameof(UnitReplicaRepository),
            new ArgumentNullException($"{nameof(context)} should not be null."));
    }

    public IUnitOfWork UnitOfWork => _context;

    public UnitReplica AddASync(UnitReplica unitReplica)
    {
        return _context.Units.Add(unitReplica).Entity;
    }

    public UnitReplica? GetById(int id)
    {
        return _context.Units.AsNoTracking().SingleOrDefault(i => i.Id == id);
    }

    public UnitReplica Update(UnitReplica unitReplica)
    {
        return _context.Units.Update(unitReplica).Entity;
    }
}