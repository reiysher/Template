using Domain.Common;
using Domain.Subscriptions;
using Domain.Subscriptions.Events;
using Microsoft.Extensions.Logging;

namespace Application.Features.Subscriptions.Projections;

// application service
public class SubscriptionDashboardProjection : BaseProjectionSource<SubscriptionDashboard, Guid>
{
    private readonly ILogger<SubscriptionDashboardProjection> _logger;

    public SubscriptionDashboardProjection(
        ILogger<SubscriptionDashboardProjection> logger,
        IProjectionStore<SubscriptionDashboard, Guid> store)
        : base(store)
    {
        _logger = logger;

        Projects<SubscriptionCreatedDomainEvent>(When);
        Projects<SubscriptionRenewedDomainEvent>(When);
        Projects<SubscriptionExpiredDomainEvent>(When);
    }

    private async Task When(SubscriptionCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var projection = new SubscriptionDashboard
        {
            Id = domainEvent.SubscriptionId,
            SubscriptionId = domainEvent.SubscriptionId,
            SubscriberId = domainEvent.PayerId,
            SubscriptionPaymentId = domainEvent.PaymentId,
            StartDate = domainEvent.StartDate,
            ExpirationDate = domainEvent.ExpirationDate,
            IsActive = true
        };

        await Store.AddAsync(projection, cancellationToken);
    }

    private async Task When(SubscriptionRenewedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var projection = await Store.GetByIdAsync(domainEvent.SubscriptionId, cancellationToken);

        if (projection == null)
        {
            _logger.LogWarning("Projection with id [{SubscriptionId}] not found", domainEvent.SubscriptionId);
            return;
        }

        projection.ExpirationDate = domainEvent.ExpirationDate;
        projection.IsActive = true;
    }

    private async Task When(SubscriptionExpiredDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var projection = await Store.GetByIdAsync(domainEvent.SubscriptionId, cancellationToken);

        if (projection == null)
        {
            _logger.LogWarning("Projection with id [{SubscriptionId}] not found", domainEvent.SubscriptionId);
            return;
        }

        projection.IsActive = false;
    }
}

