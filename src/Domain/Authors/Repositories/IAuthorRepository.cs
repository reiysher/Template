using Domain.Common.Persistence;

namespace Domain.Authors.Repositories;

public interface IAuthorRepository : IRepository<Author>
{
    Task<Author> GetById(Guid authorId, CancellationToken cancellationToken);

    void Insert(Author author);

    void Delete(Author author);
}
