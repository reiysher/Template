using Application.Abstractions.Messaging.DomainEvents;
using Domain.Common;
using MediatR;

namespace Infrastructure.Messaging;

internal class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IPublisher _publisher;

    public DomainEventDispatcher(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task Dispatch(IReadOnlyCollection<IAggregate> aggregatesWithEvents)
    {
        foreach (var aggregate in aggregatesWithEvents)
        {
            var domainEvents = aggregate.DomainEvents.ToArray();

            aggregate.ClearDomainEvents();

            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent);
            }
        }
    }
}
