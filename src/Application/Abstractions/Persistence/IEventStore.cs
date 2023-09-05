namespace Application.Abstractions.Persistence;

public interface IEventStore
{
    Task AppendEvent<TStream>(Guid streamId, object @event, long? expectedVersion = null)
        where TStream : notnull;

    Task<T> AggregateStream<T>(Guid streamId, long? atStreamVersion = null, DateTimeOffset? atTimestamp = null)
        where T : notnull;

    Task<List<object>> GetEvents(Guid streamId, long? atStreamVersion = null, DateTimeOffset? atTimestamp = null);

    //Task<IEnumerable<EventStream>> GetAllStreams();

    //EventStream CreateStream(Guid streamId, string streamName);
}
