using Domain.Common;

namespace Infrastructure.Persistence.EventSourcing;

public interface IEventStore
{
    Task<bool> Store<TStream>(Guid streamId, TStream aggregate, CancellationToken cancellationToken)
        where TStream : IAggregate;

    Task AppendEvent<TStream>(Guid streamId, IDomainEvent domainEvent, long? expectedVersion = null, CancellationToken cancellationToken = default)
        where TStream : notnull;

    Task<T> AggregateStream<T>(Guid streamId, CancellationToken cancellationToken)
        where T : IAggregate;

    Task<T> AggregateStream<T>(Guid streamId, long? atStreamVersion, DateTimeOffset? atTimestamp, CancellationToken cancellationToken)
        where T : IAggregate;

    Task<List<IDomainEvent>> GetEvents(Guid streamId, long? atStreamVersion = null, DateTimeOffset? atTimestamp = null, CancellationToken cancellationToken = default);
}
