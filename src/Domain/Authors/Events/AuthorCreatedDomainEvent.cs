using Domain.Common;

namespace Domain.Authors.Events;

public sealed record AuthorCreatedDomainEvent(Guid AuthorId) : DomainEvent();
