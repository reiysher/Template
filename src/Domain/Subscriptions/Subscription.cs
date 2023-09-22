using Domain.Common;
using Domain.Subscriptions.Events;

namespace Domain.Subscriptions;

public class Subscription : Aggregate<SubscriptionId>, IAggregate, IAggregateRoot
{
    public Guid SubscriberId { get; private set; }

    public Guid SubscriptionPaymentId { get; private set; }

    public DateTimeOffset StartDate { get; private set; }

    public DateTimeOffset ExpirationDate { get; private set; }

    public bool IsActive { get; private set; } // todo: use SubscriptionStatus (enum or enumeration)

    private Subscription()
    {
        // for ef core
    }

    public static Subscription Create(Guid paymentId, Guid payerId, int periodInMonths)
    {
        var subscription = new Subscription();

        var startDate = DateTimeOffset.UtcNow;
        var expirationDate = startDate.AddMonths(periodInMonths);

        var domainEvent = new SubscriptionCreatedDomainEvent(
            Guid.NewGuid(),
            paymentId,
            payerId,
            periodInMonths,
            startDate,
            expirationDate);

        subscription.Apply(domainEvent);
        subscription.Raise(domainEvent);

        return subscription;
    }

    public void Renew(int perionInMonths)
    {
        var expirationDate = ExpirationDate < DateTimeOffset.UtcNow
            ? DateTimeOffset.UtcNow.AddMonths(perionInMonths)
            : ExpirationDate.AddMonths(perionInMonths);

        var domainEvent = new SubscriptionRenewedDomainEvent(
            Id.Value,
            expirationDate);

        Apply(domainEvent);
        Raise(domainEvent);
    }

    public void Expire()
    {
        if (ExpirationDate < DateTimeOffset.UtcNow)
        {
            var domainEvent = new SubscriptionExpiredDomainEvent(Id.Value);

            Apply(domainEvent);
            Raise(domainEvent);
        }
    }

    public void Apply(IDomainEvent domainEvent)
    {
        When((dynamic)domainEvent);
    }

    private void When(SubscriptionCreatedDomainEvent domainEvent)
    {
        Id = new SubscriptionId(domainEvent.SubscriptionId);
        SubscriberId = domainEvent.PayerId;
        SubscriptionPaymentId = domainEvent.PaymentId;
        StartDate = domainEvent.StartDate;
        ExpirationDate = domainEvent.ExpirationDate;
        IsActive = true;
    }

    private void When(SubscriptionRenewedDomainEvent domainEvent)
    {
        ExpirationDate = domainEvent.ExpirationDate;
        IsActive = true;
    }

    private void When(SubscriptionExpiredDomainEvent domainEvent)
    {
        IsActive = false;
    }
}
