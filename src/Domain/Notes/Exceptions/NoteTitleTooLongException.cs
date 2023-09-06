using Domain.Common;

namespace Domain.Notes.Exceptions;

public sealed class NoteTitleTooLongException : DomainException
{
    public string Title { get; private set; }

    public NoteTitleTooLongException(string title)
    {
        Title = title;
    }
}
