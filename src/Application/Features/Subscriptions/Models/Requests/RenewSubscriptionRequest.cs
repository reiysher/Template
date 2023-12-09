namespace Application.Features.Subscriptions.Models.Requests;

public record RenewSubscriptionRequest(Guid SubscriptionId, int PerionInMonths);
