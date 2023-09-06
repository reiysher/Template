using Domain.Common;

namespace Domain.Notes.Exceptions;

public sealed class NoteNotFoundException : DomainException
{
    public Guid NoteId { get; private set; }

    public NoteNotFoundException(NoteId noteId)
    {
        NoteId = noteId.Value;
    }
}
