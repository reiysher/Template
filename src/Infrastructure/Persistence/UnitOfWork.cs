using Application.Abstractions.Messaging.DomainEvents;
using Application.Abstractions.Persistence;
using Domain.Common;
using Infrastructure.Persistence.Contexts;
using System.Data;

namespace Infrastructure.Persistence;

internal class UnitOfWork(
    ApplicationDbContext dbContext,
    IDomainEventDispatcher dispatcher)
    : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IDomainEventDispatcher _dispatcher = dispatcher;

    public async Task<int> CommitAsync(CancellationToken cancellationToken)
    {
        await SendDomainEventsAsync();

        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        return result;
    }

    private Task SendDomainEventsAsync()
    {
        var aggregatesWithEvents = _dbContext
            .ChangeTracker
            .Entries<IAggregate>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Count > 0)
            .ToArray();

        return _dispatcher.Dispatch(aggregatesWithEvents);
    }
}
