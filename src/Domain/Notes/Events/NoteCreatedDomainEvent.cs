using Domain.Authors;
using Domain.Common;

namespace Domain.Notes.Events;

public sealed record NoteCreatedDomainEvent(Guid AuthorId, NoteId NoteId) : DomainEvent();
