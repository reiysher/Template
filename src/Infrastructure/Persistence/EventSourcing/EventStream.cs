namespace Infrastructure.Persistence.EventSourcing;

internal class EventStream
{
    public Guid Id { get; set; }

    public string Type { get; set; } = default!;

    public long Version { get; set; }

    public ICollection<EventData>? Events { get; set; }
}
