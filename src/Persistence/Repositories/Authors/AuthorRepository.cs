using Domain.Authors;
using Domain.Authors.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories.Authors;

internal class AuthorRepository(ApplicationDbContext context) : IAuthorRepository
{
    private readonly ApplicationDbContext _context = context;

    public Task<Author?> GetById(Guid authorId, CancellationToken cancellationToken)
    {
        return _context.Set<Author>().SingleOrDefaultAsync(author => author.Id == authorId, cancellationToken);
    }

    public void Add(Author author)
    {
        _context.Set<Author>().Add(author);
    }

    public void Delete(Author author)
    {
        _context.Set<Author>().Remove(author);
    }
}
