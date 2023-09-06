using Application.Abstractions.Messaging.Commands;
using Application.Abstractions.Persistence;
using Application.Features.Subscriptions.Repositories;
using Domain.Subscriptions;

namespace Application.Features.Subscriptions.Commands.Create;

internal class CreateSubscriptionCommandHandler : ICommandHandler<CreateSubscriptionCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISubscriptionRepository _repository;

    public CreateSubscriptionCommandHandler(IUnitOfWork unitOfWork, ISubscriptionRepository repository)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateSubscriptionCommand command, CancellationToken cancellationToken)
    {
        var subscription = Subscription.Create(command.PaymentId, command.PayerId, command.PeriodInMonths);

        await _repository.Save(subscription, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return subscription.Id.Value;
    }
}
