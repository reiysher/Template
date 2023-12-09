using Application.Abstractions.Messaging.DomainEvents;
using Application.Abstractions.Persistence;
using Domain.Common;
using Infrastructure.Persistence.Contexts;

namespace Infrastructure.Persistence;

internal class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IDomainEventDispatcher _dispatcher;

    public UnitOfWork(
        ApplicationDbContext dbContext,
        IDomainEventDispatcher dispatcher)
    {
        _dbContext = dbContext;
        _dispatcher = dispatcher;
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken)
    {
        await SendDomainEventsAsync();

        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        // Если удалить агрегат, то ивенты с него не соберуться.
        // Для интеграции с другими ограниченными контекстами,
        // Необходимо мягкое удаление, что бы сохранить "ссылки"
        // Так как во внешних системах могут быть id этих сущностей.
        // todo: ISoftDelete

        // await SendDomainEventsAsync();

        return result;
    }

    private Task SendDomainEventsAsync()
    {
        var aggregatesWithEvents = _dbContext
            .ChangeTracker
            .Entries<IAggregate>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToArray();

        return _dispatcher.Dispatch(aggregatesWithEvents);
    }
}
