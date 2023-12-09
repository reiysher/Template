using Application.Abstractions.Messaging.Commands;
using Application.Abstractions.Persistence;
using Application.Features.Subscriptions.Repositories;
using Domain.Subscriptions;

namespace Application.Features.Subscriptions.Commands.Create;

internal class CreateSubscriptionCommandHandler(
    IUnitOfWork unitOfWork,
    ISubscriptionRepository repository,
    TimeProvider timeProvider)
    : ICommandHandler<CreateSubscriptionCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ISubscriptionRepository _repository = repository;
    private readonly TimeProvider _timeProvider = timeProvider;

    public async Task<Guid> Handle(CreateSubscriptionCommand command, CancellationToken cancellationToken)
    {
        var subscription = new Subscription(command.PaymentId, command.PayerId, command.PeriodInMonths, _timeProvider.GetUtcNow());

        await _repository.SaveEvents(subscription, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return subscription.Id.Value;
    }
}
