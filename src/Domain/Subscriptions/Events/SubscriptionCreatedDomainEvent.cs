using Domain.Common;

namespace Domain.Subscriptions.Events;

public sealed record SubscriptionCreatedDomainEvent(
    Guid SubscriptionId,
    Guid PaymentId,
    Guid PayerId,
    int Period,
    DateTimeOffset StartDate,
    DateTimeOffset ExpirationDate)
    : DomainEvent();
