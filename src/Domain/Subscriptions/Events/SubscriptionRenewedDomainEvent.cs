using Domain.Common;

namespace Domain.Subscriptions.Events;

public sealed record SubscriptionRenewedDomainEvent(
    Guid SubscriptionId,
    DateTimeOffset ExpirationDate)
    : DomainEvent();
