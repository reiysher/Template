using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System.Text.Json;

namespace Persistence.EventSourcing;

internal class EventStore : IEventStore
{
    private readonly ApplicationDbContext _dbContext;
    private readonly JsonSerializerOptions _jsonOptions;

    public EventStore(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }

    public async Task AppendEvent<TStream>(Guid streamId, IDomainEvent domainEvent, long? expectedVersion = null)
        where TStream : notnull
    {
        var stream = await GetStreamById(streamId);
        stream ??= CreateStream(streamId, typeof(TStream).AssemblyQualifiedName!);

        if (expectedVersion.HasValue && stream.Version != expectedVersion)
        {
            throw new InvalidOperationException("Concurrency conflict. Stream has been modified by another process.");
        }

        var eventPayloadJson = JsonSerializer.Serialize(domainEvent, domainEvent.GetType(), _jsonOptions);

        var eventData = new EventData
        {
            Id = Guid.NewGuid(),
            Type = domainEvent.GetType().AssemblyQualifiedName!,
            Payload = eventPayloadJson,
            Created = DateTimeOffset.UtcNow,
            StreamId = streamId,
            Version = stream.Version + 1
        };

        stream.Version = eventData.Version;

        _dbContext.Set<EventData>().Add(eventData);
    }

    public Task<T> AggregateStream<T>(Guid streamId, CancellationToken cancellationToken)
        where T : IAggregate
    {
        return AggregateStream<T>(streamId, null, null, cancellationToken);
    }


    public async Task<T> AggregateStream<T>(Guid streamId, long? atStreamVersion, DateTimeOffset? atTimestamp, CancellationToken cancellationToken)
        where T : IAggregate
    {
        var aggregate = (T)Activator.CreateInstance(typeof(T), true)!;

        var events = await GetEvents(streamId, atStreamVersion, atTimestamp);
        var version = 0;

        foreach (var @event in events)
        {
            aggregate.Apply(@event);
            aggregate.Version(++version);
        }

        return aggregate;
    }

    public async Task<List<IDomainEvent>> GetEvents(Guid streamId, long? atStreamVersion = null, DateTimeOffset? atTimestamp = null)
    {
        var events = await _dbContext
            .Set<EventData>()
            .Where(e => e.StreamId == streamId)
            .Where(e => !atStreamVersion.HasValue || e.Version <= atStreamVersion)
            .Where(e => !atTimestamp.HasValue || e.Created <= atTimestamp)
            .OrderBy(e => e.Version)
            .ToListAsync();

        var deserializedEvents = new List<IDomainEvent>();

        foreach (var eventData in events)
        {
            var eventType = Type.GetType(eventData.Type);

            if (eventType == null)
            {
                throw new InvalidOperationException($"Event type {eventData.Type} not found.");
            }

            var eventPayload = JsonSerializer.Deserialize(eventData.Payload, eventType, _jsonOptions) as IDomainEvent;

            if (eventPayload is not null)
            {
                deserializedEvents.Add(eventPayload);
            }
        }

        return deserializedEvents;
    }

    public async Task<IEnumerable<EventStream>> GetAllStreams()
    {
        return await _dbContext.Set<EventStream>().Include(s => s.Events).ToListAsync();
    }

    public EventStream CreateStream(Guid streamId, string streamName)
    {
        var stream = new EventStream
        {
            Id = streamId,
            Type = streamName,
            Events = new List<EventData>()
        };

        _dbContext.Set<EventStream>().Add(stream);

        return stream;
    }

    private async Task<EventStream?> GetStreamById(Guid streamId)
    {
        return await _dbContext.Set<EventStream>().FirstOrDefaultAsync(s => s.Id == streamId);
    }
}
