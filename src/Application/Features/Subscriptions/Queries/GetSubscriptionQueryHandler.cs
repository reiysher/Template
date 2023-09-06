using Application.Abstractions.Messaging.Queries;
using Application.Features.Subscriptions.Repositories;
using Domain.Subscriptions;

namespace Application.Features.Subscriptions.Queries;

internal class GetSubscriptionQueryHandler : IQueryHandler<GetSubscriptionQuery, Subscription>
{
    private readonly ISubscriptionRepository _repository;

    public GetSubscriptionQueryHandler(ISubscriptionRepository repository)
    {
        _repository = repository;
    }

    public Task<Subscription> Handle(GetSubscriptionQuery query, CancellationToken cancellationToken)
    {
        return _repository.GetById(query.SubscriptionId, cancellationToken);
    }
}
