using Application.Abstractions.Messaging.Queries;
using Domain.Subscriptions;

namespace Application.Features.Subscriptions.Queries;

public record GetSubscriptionQuery(Guid SubscriptionId) : IQuery<Subscription>;
