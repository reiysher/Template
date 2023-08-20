using Domain.Common;
using MediatR;

namespace Application.Abstractions.Messaging.DomainEvents;

public interface IDomainEventHandler<TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
}
