using Domain.Common;
using Domain.Notes.Events;

namespace Domain.Notes;

public sealed class Note : Aggregate<NoteId>, IAggregateRoot
{
    internal const int TitleMaxLenth = 10;

    public Guid AuthorId { get; private set; } = default!;

    public string Title { get; private set; } = default!;

    public string Content { get; private set; } = default!;

    private Note()
    {
        // for ef core
    }

    internal Note(Guid authorId, string title, string content)
    {
        Id = new NoteId(Guid.NewGuid());
        AuthorId = authorId;
        Title = title;
        Content = content;

        AddDomainEvent(new NoteCreatedDomainEvent(authorId, Id));
    }

    public void Delete()
    {
        AddDomainEvent(new NoteDeletedDomainEvent(Id));
    }
}
