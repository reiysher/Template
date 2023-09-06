namespace Persistence.EventSourcing;

internal class EventData
{
    public Guid Id { get; set; }

    public string Payload { get; set; } = default!;

    public Guid StreamId { get; set; }

    public string Type { get; set; } = default!;

    public long Version { get; set; }

    public DateTimeOffset Created { get; set; }
}
