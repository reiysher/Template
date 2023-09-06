﻿using MediatR;

namespace Domain.Common;

public interface IDomainEvent : INotification
{
    Guid Id { get; }

    DateTimeOffset OccurredOn { get; }
}
