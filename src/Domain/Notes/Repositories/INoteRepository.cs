using Domain.Authors;
using Domain.Common.Persistence;

namespace Domain.Notes.Repositories;

public interface INoteRepository : IRepository<Note>
{
    Task<Note?> GetById(NoteId noteId, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<Note>> GetByAuthorId(Guid authorId, CancellationToken cancellationToken);

    void Add(Note note);

    void Delete(Note note);
}
