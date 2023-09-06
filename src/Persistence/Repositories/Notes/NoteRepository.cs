using Domain.Authors;
using Domain.Notes;
using Domain.Notes.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories.Notes;

internal class NoteRepository : INoteRepository
{
    private readonly ApplicationDbContext _context;

    public NoteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Note?> GetById(
        NoteId noteId,
        CancellationToken cancellationToken)
    {
        return _context
            .Set<Note>()
            .SingleOrDefaultAsync(note => note.Id == noteId, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Note>> GetByAuthorId(
        Guid authorId,
        CancellationToken cancellationToken)
    {
        return await _context
            .Set<Note>()
            .Where(note => note.AuthorId == authorId)
            .ToListAsync(cancellationToken);
    }

    public void Add(Note note)
    {
        _context.Set<Note>().Add(note);
    }

    public void Delete(Note note)
    {
        _context.Set<Note>().Remove(note);
    }
}
