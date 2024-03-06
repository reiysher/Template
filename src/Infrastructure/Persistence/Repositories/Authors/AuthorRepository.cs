using Domain.Authors;
using Domain.Authors.Repositories;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Authors;

internal class AuthorRepository(ApplicationDbContext context) : IAuthorRepository
{
    public async Task<IReadOnlyCollection<Author>> Search(SearchAuthorCriteria criteria, CancellationToken cancellationToken)
    {
        var query = context.Set<Author>().AsQueryable();

        query = Apply(query, criteria.Filter);

        return await query.ToListAsync(cancellationToken);
    }

    public Task<Author?> GetById(Guid authorId, CancellationToken cancellationToken)
    {
        return context.Set<Author>().SingleOrDefaultAsync(author => author.Id == authorId, cancellationToken);
    }

    public void Insert(Author author)
    {
        context.Set<Author>().Add(author);
    }

    public void Delete(Author author)
    {
        context.Set<Author>().Remove(author);
    }

    private static IQueryable<Author> Apply(IQueryable<Author> query, AuthorFilter? filter)
    {
        if (filter?.AuthorIds is not null && filter.AuthorIds.Count > 0)
        {
            query = query.Where(author => filter.AuthorIds.Contains(author.Id));
        }

        return query;
    }
}
