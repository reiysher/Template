namespace Messaging.IntegrationEvents.Abstractions;

/// <summary>
/// Author deleted
/// </summary>
public interface IAuthorDeletedIntegrationEvent
{
    /// <summary>
    /// Author id
    /// </summary>
    Guid AuthorId { get; }
}
