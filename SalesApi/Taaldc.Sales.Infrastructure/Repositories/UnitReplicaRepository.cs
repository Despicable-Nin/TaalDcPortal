using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SeedWork;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;
using Taaldc.Sales.Domain.Exceptions;

namespace Taaldc.Sales.Infrastructure.Repositories;

public class UnitReplicaRepository : IUnitReplicaRepository
{
    private readonly SalesDbContext _context;
    private readonly ILogger<UnitReplicaRepository> _logger;

    public UnitReplicaRepository(SalesDbContext context, ILogger<UnitReplicaRepository> logger)
    {
        _context = context ?? throw new SalesDomainException(nameof(UnitReplicaRepository),
            new ArgumentNullException($"{nameof(context)} should not be null."));

        _logger = logger;
    }

    public IUnitOfWork UnitOfWork => _context;

    public UnitReplica AddASync(UnitReplica unitReplica)
    {
        return _context.Units.Add(unitReplica).Entity;
    }

    /// <summary>
    /// Only use this if it requires to update the ENTITY
    /// For other use, preferred usage is UnitQueries
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public UnitReplica GetById(int id)
    {
        return _context.Units.SingleOrDefault(i => i.Id == id);
    }

    public UnitReplica Update(UnitReplica unitReplica)
    {
        return _context.Units.Update(unitReplica).Entity;
    }
}