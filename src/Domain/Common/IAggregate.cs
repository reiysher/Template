namespace Domain.Common;

public interface IAggregate
{
    int Version { get; set; }

    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

    void Apply(IDomainEvent domainEvent);
}
