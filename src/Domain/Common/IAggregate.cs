namespace Domain.Common;

public interface IAggregate
{
    void Apply(IDomainEvent domainEvent);

    void Version(int version);
}
