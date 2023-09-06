namespace Messaging.IntegrationEvents.Abstractions;

public interface IAuthorDeletedIntegrationEvent
{
    Guid AuthorId { get; }
}
