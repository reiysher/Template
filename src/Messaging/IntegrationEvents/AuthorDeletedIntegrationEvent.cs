using Messaging.IntegrationEvents.Abstractions;

namespace Messaging.IntegrationEvents;

public sealed record AuthorDeletedIntegrationEvent(Guid AuthorId) : IAuthorDeletedIntegrationEvent;
