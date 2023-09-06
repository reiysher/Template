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
        return _eventStore.AggregateStream<Subscription>(subscriptionId, cancellationToken: cancellationToken);
    }

    public async Task Save(Subscription subscription, CancellationToken cancellationToken)
    {
        foreach (var domainEvent in subscription.DomainEvents)
        {
            await _eventStore.AppendEvent<Subscription>(subscription.Id.Value, domainEvent);
        }
    }
}
