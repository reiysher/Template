namespace Domain.Common;

public interface IProjectionSource
{
    bool HasHandler(Type eventType);

    Task HandleAsync(IDomainEvent domainEvent, CancellationToken cancellationToken);
}

public abstract class BaseProjectionSource<TProjection, TId> : IProjectionSource
    where TProjection : IProjection<TId>
    where TId : notnull
{
    private readonly Dictionary<Type, Func<IDomainEvent, CancellationToken, Task>> _handlers = new();
    protected readonly IProjectionStore<TProjection, TId> Store;

    protected BaseProjectionSource(IProjectionStore<TProjection, TId> store)
    {
        Store = store;
    }
    public bool HasHandler(Type eventType)
    {
        return _handlers.ContainsKey(eventType);
    }

    protected void Projects<TEvent>(Func<TEvent, CancellationToken, Task> handler)
        where TEvent : IDomainEvent
    {
        _handlers.Add(typeof(TEvent), (domainEvent, cancellationToken) => handler((TEvent)domainEvent, cancellationToken));
    }

    public async Task HandleAsync(IDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        await _handlers[domainEvent.GetType()](domainEvent, cancellationToken);
    }
}
