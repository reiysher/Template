using Domain.Notes.Repositories;
using Domain.Notes.Services;

namespace Infrastructure.Services.Notes;

internal sealed class NoteService : INoteService
{
    private readonly INoteRepository _repository;

    public NoteService(INoteRepository repository)
    {
        _repository = repository;
    }

    public async Task DeleteByAuthodId(Guid authorId, CancellationToken cancellationToken)
    {
        var notes = await _repository.GetByAuthorId(authorId, cancellationToken);

        foreach (var note in notes)
        {
            note.Delete();
            _repository.Delete(note);
        }
    }
}
