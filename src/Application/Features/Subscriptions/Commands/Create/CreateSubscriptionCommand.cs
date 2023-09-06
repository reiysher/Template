using Application.Abstractions.Messaging.Commands;

namespace Application.Features.Subscriptions.Commands.Create;

public record CreateSubscriptionCommand(Guid PaymentId, Guid PayerId, int PeriodInMonths) : ICommand<Guid>;
