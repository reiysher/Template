using Domain.Common.Persistence;

namespace Domain.Authors.Repositories;

public interface IAuthorRepository : IRepository<Author>
{
    Task<Author> GetById(Guid authorId, CancellationToken cancellationToken);

    void Add(Author author);

    void Delete(Author author);
}
