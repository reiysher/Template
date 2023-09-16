using Domain.Common;

namespace Domain.Subscriptions;

// read model
public class SubscriptionDashboard : IProjection<Guid>
{
    public Guid Id { get; set; }

    public Guid SubscriptionId { get; set; }

    public Guid SubscriberId { get; set; }

    public Guid SubscriptionPaymentId { get; set; }

    public DateTimeOffset StartDate { get; set; }

    public DateTimeOffset ExpirationDate { get; set; }

    public bool IsActive { get; set; }
}
