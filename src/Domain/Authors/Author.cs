using Ardalis.GuardClauses;
using Domain.Authors.Events;
using Domain.Common;
using Domain.Notes;
using Domain.Notes.Guards;

namespace Domain.Authors;

public sealed class Author : Aggregate<Guid>, IAggregateRoot
{
    public FullName? FullName { get; private set; }

    public DateTime BirthDay { get; private set; }

    public DateTime Created { get; private set; }

    private Author()
    {
        // for ef core
    }

    private Author(string firstName, DateTime birthDay)
    {
        Id = Guid.NewGuid();
        FullName = new FullName(firstName, String.Empty, String.Empty);
        BirthDay = birthDay;
        Created = DateTime.UtcNow;

        AddDomainEvent(new AuthorCreatedDomainEvent(Id));
    }

    public static Author Create(string firstName, DateTime birthDay)
    {
        // todo: guards?

        var author = new Author(firstName, birthDay);
        

        return author;
    }

    public void Delete()
    {
        AddDomainEvent(new AuthorDeletedDomainEvent(Id));
    }

    public string GetFullName()
    {
        Guard.Against.Null(FullName);

        return FullName.GetFullName();
    }

    public Note WriteNote(string title, string content)
    {
        Guard.Against.TitleTooLong(title, Note.TitleMaxLenth);

        var note = new Note(Id, title, content);

        return note;
    }
}
