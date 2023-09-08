using Domain.Subscriptions;

namespace Application.Features.Subscriptions.Repositories;

public interface ISubscriptionRepository
{
    Task<Subscription> GetById(Guid subscriptionId, CancellationToken cancellationToken);

    // todo: сбор ивентов для сохранения или тут или сделать абстракцию IStoredDomainEvent(над названием подумать) и в SaveChanges сохранять их
    Task SaveEvents(Subscription subscription, CancellationToken cancellationToken);
}
