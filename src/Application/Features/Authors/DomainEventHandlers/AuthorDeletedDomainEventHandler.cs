using Application.Abstractions.Messaging.DomainEvents;
using Domain.Authors.Events;
using MassTransit;
using Messaging.IntegrationEvents;
using Messaging.IntegrationEvents.Abstractions;

namespace Application.Features.Authors.DomainEventHandlers;

internal sealed class AuthorDeletedDomainEventHandler : IDomainEventHandler<AuthorDeletedDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public AuthorDeletedDomainEventHandler(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public Task Handle(AuthorDeletedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var integrationEvent = new AuthorDeletedIntegrationEvent(domainEvent.AuthorId);

        return _publishEndpoint.Publish<IAuthorDeletedIntegrationEvent>(integrationEvent, cancellationToken);
    }
}
