namespace Domain.Common;

public interface IAggregate
{
    int Version { get; set; }

    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

    void ClearDomainEvents();

    void Apply(IDomainEvent domainEvent);
}
