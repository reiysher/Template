using Application.Abstractions.Messaging.Commands;
using Application.Abstractions.Persistence;
using Application.Features.Subscriptions.Repositories;

namespace Application.Features.Subscriptions.Commands.Renew;

internal class RenewSubscriptionCommandHandler(
    IUnitOfWork unitOfWork,
    ISubscriptionRepository repository,
    TimeProvider timeProvider)
    : ICommandHandler<RenewSubscriptionCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ISubscriptionRepository _repository = repository;
    private readonly TimeProvider _timeProvider = timeProvider;

    public async Task Handle(RenewSubscriptionCommand command, CancellationToken cancellationToken)
    {
        var subscription = await _repository.GetById(command.SubscriptionId, cancellationToken);

        subscription.Renew(command.PerionInMonths, _timeProvider.GetUtcNow());

        await _repository.SaveEvents(subscription, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}
