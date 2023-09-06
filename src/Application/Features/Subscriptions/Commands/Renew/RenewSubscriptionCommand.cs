using Application.Abstractions.Messaging.Commands;

namespace Application.Features.Subscriptions.Commands.Renew;

public record RenewSubscriptionCommand(Guid SubscriptionId, int PerionInMonths) : ICommand;
