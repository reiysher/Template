using Domain.Common;
using Domain.Subscriptions.Events;

namespace Domain.Subscriptions;

public class Subscription : Aggregate<SubscriptionId>
{
    public Guid SubscriberId { get; private set; }

    public Guid SubscriptionPaymentId { get; private set; }

    public DateTimeOffset StartDate { get; private set; }

    public DateTimeOffset ExpirationDate { get; private set; }

    public bool IsActive { get; private set; } // todo: use SubscriptionStatus (enum or enumeration)

    private Subscription()
    {
        // for ORM
    }

    public Subscription(Guid paymentId, Guid payerId, int periodInMonths, DateTimeOffset startDate)
    {
        var expirationDate = startDate.AddMonths(periodInMonths);

        var domainEvent = new SubscriptionCreatedDomainEvent(
            Guid.NewGuid(),
            paymentId,
            payerId,
            periodInMonths,
            startDate,
            expirationDate);

        Apply(domainEvent);
        AddDomainEvent(domainEvent);
    }

    public void Renew(int perionInMonths, DateTimeOffset utcNow)
    {
        var expirationDate = ExpirationDate < utcNow
            ? utcNow.AddMonths(perionInMonths)
            : ExpirationDate.AddMonths(perionInMonths);

        var domainEvent = new SubscriptionRenewedDomainEvent(
            Id.Value,
            expirationDate);

        Apply(domainEvent);
        AddDomainEvent(domainEvent);
    }

    public void Expire(DateTimeOffset utcNow)
    {
        if (ExpirationDate < utcNow)
        {
            var domainEvent = new SubscriptionExpiredDomainEvent(Id.Value);

            Apply(domainEvent);
            AddDomainEvent(domainEvent);
        }
    }

    public override void Apply(IDomainEvent domainEvent)
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
