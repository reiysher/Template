using MediatR;

namespace IntegrationTests.Utils.Spies;

internal class PublisherSpy : IPublisher
{
    private readonly List<INotification> _sentEvents = [];

    public Task Publish(object notification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : INotification
    {
        _sentEvents.Add(notification);

        return Task.CompletedTask;
    }

    public int SentEventsCount()
    {
        return _sentEvents.Count;
    }
}
