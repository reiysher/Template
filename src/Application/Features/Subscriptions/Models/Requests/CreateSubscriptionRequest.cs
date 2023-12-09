namespace Application.Features.Subscriptions.Models.Requests;

public record CreateSubscriptionRequest(Guid PaymentId, Guid PayerId, int PeriodInMonths);
