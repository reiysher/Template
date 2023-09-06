using Ardalis.GuardClauses;
using Domain.Notes.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Notes.Guards;

// todo: other clauses
public static class NoteGuard
{
    public static Note NoteNotFound(
        this IGuardClause guardClause,
        [NotNull] Note? note,
        NoteId noteId)
    {
        if (note is null)
        {
            throw new NoteNotFoundException(noteId);
        }

        return note;
    }

    public static string TitleTooLong(
        this IGuardClause guardClause,
        [NotNull] string title,
        int lenght = 0)
    {
        if (title.Length > lenght)
        {
            throw new NoteTitleTooLongException(title);
        }

        return title;
    }
}
