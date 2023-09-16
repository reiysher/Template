using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common;

public abstract class Aggregate<TId> : Entity<TId>, IAggregate
    where TId : notnull
{
    private readonly List<IDomainEvent> _domainEvents = new();

    [NotMapped]
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public int Version { get; set; }

    public void Raise(IDomainEvent eventItem)
    {
        _domainEvents.Add(eventItem);
        Version++;
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public virtual void Apply(IDomainEvent domainEvent)
    {
        // empty by default
    }
}
