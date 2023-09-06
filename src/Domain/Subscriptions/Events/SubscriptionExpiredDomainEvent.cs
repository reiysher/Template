using Domain.Common;

namespace Domain.Subscriptions.Events;

public sealed record SubscriptionExpiredDomainEvent(
    Guid SubscriptionId)
    : DomainEvent();
