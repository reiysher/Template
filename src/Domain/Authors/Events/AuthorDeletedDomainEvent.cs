using Domain.Common;

namespace Domain.Authors.Events;

public sealed record AuthorDeletedDomainEvent(Guid AuthorId) : DomainEvent();
