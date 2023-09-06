namespace Domain.Common;

public abstract record DomainEvent : IDomainEvent
{
    public Guid Id { get; }

    public DateTimeOffset OccurredOn { get; }

    public DomainEvent()
    {
        Id = Guid.NewGuid();
        OccurredOn = DateTimeOffset.UtcNow;
    }
}
