using Domain.Subscriptions;

namespace Application.Features.Subscriptions.Repositories;

public interface ISubscriptionRepository
{
    Task<Subscription> GetById(Guid subscriptionId, CancellationToken cancellationToken);

    Task SaveEvents(Subscription subscription, CancellationToken cancellationToken);
}
