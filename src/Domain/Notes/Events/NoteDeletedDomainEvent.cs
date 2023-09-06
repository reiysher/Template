using Domain.Common;

namespace Domain.Notes.Events;

public sealed record NoteDeletedDomainEvent(NoteId NoteId) : DomainEvent();
