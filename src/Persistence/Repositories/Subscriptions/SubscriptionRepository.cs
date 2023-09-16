using Application.Features.Subscriptions.Repositories;
using Domain.Subscriptions;
using Persistence.EventSourcing;

namespace Persistence.Repositories.Subscriptions;

internal class SubscriptionRepository : ISubscriptionRepository
{
    private readonly IEventStore _eventStore;

    public SubscriptionRepository(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public Task<Subscription> GetById(Guid subscriptionId, CancellationToken cancellationToken)
    {
        return _eventStore.AggregateStream<Subscription>(subscriptionId, cancellationToken);
    }

    public Task SaveEvents(Subscription subscription, CancellationToken cancellationToken)
    {
        return _eventStore.Store(subscription.Id.Value, subscription, cancellationToken);
    }
}
