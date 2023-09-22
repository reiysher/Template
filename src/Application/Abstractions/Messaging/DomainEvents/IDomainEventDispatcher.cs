using Application.Abstractions.Common;
using Domain.Common;

namespace Application.Abstractions.Messaging.DomainEvents;

// todo: scrutor
public interface IDomainEventDispatcher : ITransientService
{
    Task Dispatch(IReadOnlyCollection<IAggregate> aggregatesWithEvents);
}
