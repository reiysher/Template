using Ardalis.GuardClauses;
using Domain.Authors;
using Domain.Authors.Repositories;
using Infrastructure.Persistence.Contexts;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Repositories.Authors;

internal class MongoDbAuthorRepository : IAuthorRepository
{
    private readonly MongoDbContext _dbContext;

    public MongoDbAuthorRepository(MongoDbContext dbContext, string collectionName = "")
    {
        _dbContext = Guard.Against.Null(dbContext, nameof(dbContext));
    }

    public void Insert(Author author)
    {
        _dbContext.Authors.InsertOne(author);
    }

    public Task AddAsync(Author author, CancellationToken cancellationToken)
    {
        return _dbContext.Authors.InsertOneAsync(author, new InsertOneOptions { }, cancellationToken);
    }

    public Task UpdateAsync(Author author, CancellationToken cancellationToken)
    {
        return _dbContext.Authors.ReplaceOneAsync(p => p.Id == author.Id, author, new ReplaceOptions() { IsUpsert = false }, cancellationToken);
    }

    public void Delete(Author author)
    {
        _dbContext.Authors.DeleteOne(a => a.Id == author.Id);
    }

    public Task DeleteAsync(Author author, CancellationToken cancellationToken)
    {
        return _dbContext.Authors.DeleteOneAsync(a => a.Id == author.Id, cancellationToken);
    }

    public Task<Author> GetById(Guid authorId, CancellationToken cancellationToken)
    {
        return _dbContext.Authors.Find(a => a.Id == authorId).FirstOrDefaultAsync(cancellationToken);
    }

    public Task<IReadOnlyCollection<Author>> GetList(int page = 1, int size = int.MaxValue, CancellationToken cancellationToken = default)
    {
        return _dbContext.Authors.Find("{}")
            .SortBy(a => a.BirthDay) // обязательно надо сортировать.
            .Skip((page - 1) * size)
            .Limit(size)
            .ToListAsync(cancellationToken)
            .ContinueWith(action => action.Result as IReadOnlyCollection<Author>);
    }
}
