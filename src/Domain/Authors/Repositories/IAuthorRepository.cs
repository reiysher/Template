using Domain.Common.Persistence;

namespace Domain.Authors.Repositories;

public interface IAuthorRepository : IRepository<Author>
{
    Task<IReadOnlyCollection<Author>> Search(SearchAuthorCriteria criteria, CancellationToken cancellationToken);

    Task<Author?> GetById(Guid authorId, CancellationToken cancellationToken);

    void Insert(Author author);

    void Delete(Author author);
}
