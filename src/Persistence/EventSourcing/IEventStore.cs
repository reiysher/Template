using Domain.Common;

namespace Persistence.EventSourcing;

public interface IEventStore
{
    Task AppendEvent<TStream>(Guid streamId, IDomainEvent domainEvent, long? expectedVersion = null)
        where TStream : notnull;

    Task<T> AggregateStream<T>(Guid streamId, CancellationToken cancellationToken)
        where T : IAggregate;

    Task<T> AggregateStream<T>(Guid streamId, long? atStreamVersion, DateTimeOffset? atTimestamp, CancellationToken cancellationToken)
        where T : IAggregate;

    Task<List<IDomainEvent>> GetEvents(Guid streamId, long? atStreamVersion = null, DateTimeOffset? atTimestamp = null);

    //Task<IEnumerable<EventStream>> GetAllStreams();

    //EventStream CreateStream(Guid streamId, string streamName);
}
