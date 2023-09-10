using Application.Abstractions.Messaging.DomainEvents;
using Application.Abstractions.Persistence;
using Domain.Common;
using Persistence.Contexts;

namespace Persistence;

internal class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly IDomainEventDispatcher _dispatcher;

    public UnitOfWork(
        ApplicationDbContext context,
        IDomainEventDispatcher dispatcher)
    {
        _context = context;
        _dispatcher = dispatcher;
    }

    public async Task<int> SaveChanges(CancellationToken cancellationToken)
    {
        var result = await _context.SaveChangesAsync(cancellationToken);

        // Если удалить агрегат, то ивенты с него не соберуться.
        // Для интеграции с другими ограниченными контекстами,
        // Необходимо мягкое удаление, что бы сохранить "ссылки"
        // Так как во внешних системах могут быть id этих сущностей.
        // todo: ISoftDelete

        await SendDomainEventsAsync();

        return result;
    }

    private Task SendDomainEventsAsync()
    {
        var entitiesWithEvents = _context
            .ChangeTracker
            .Entries<Entity>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToArray();

        return _dispatcher.Dispatch(entitiesWithEvents);
    }
}
