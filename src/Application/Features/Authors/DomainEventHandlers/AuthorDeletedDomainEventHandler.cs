using Application.Abstractions.Messaging.DomainEvents;
using Domain.Authors.Events;
using MassTransit;
using Messaging.IntegrationEvents;
using Messaging.IntegrationEvents.Abstractions;

namespace Application.Features.Authors.DomainEventHandlers;

internal sealed class AuthorDeletedDomainEventHandler : IDomainEventHandler<AuthorDeletedDomainEvent>
{
    private readonly IBus _mesageBus;

    public AuthorDeletedDomainEventHandler(IBus mesageBus)
    {
        _mesageBus = mesageBus;
    }

    public Task Handle(AuthorDeletedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var integrationEvent = new AuthorDeletedIntegrationEvent(domainEvent.AuthorId);

        return _mesageBus.Publish<IAuthorDeletedIntegrationEvent>(integrationEvent, cancellationToken);
    }
}
