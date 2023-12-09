namespace Infrastructure.Persistence.EventSourcing;

public sealed class EventStoreConcurrencyException : Exception
{
    public Guid StreamId { get; private set; }

    public EventStoreConcurrencyException(Guid streamId)
        : base("Concurrency conflict. Stream has been modified by another process.")
    {
        StreamId = streamId;
    }
}