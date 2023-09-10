using Application.Abstractions.Messaging.Commands;
using Application.Abstractions.Persistence;
using Application.Features.Subscriptions.Repositories;

namespace Application.Features.Subscriptions.Commands.Renew;

internal class RenewSubscriptionCommandHandler : ICommandHandler<RenewSubscriptionCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISubscriptionRepository _repository;

    public RenewSubscriptionCommandHandler(IUnitOfWork unitOfWork, ISubscriptionRepository repository)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task Handle(RenewSubscriptionCommand command, CancellationToken cancellationToken)
    {
        var subscription = await _repository.GetById(command.SubscriptionId, cancellationToken);

        subscription.Renew(command.PerionInMonths);

        await _repository.SaveEvents(subscription, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}
