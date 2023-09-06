using Domain.Authors;
using MongoDB.Driver;

namespace Persistence.Contexts;

public class MongoDbContext
{
    protected readonly IMongoDatabase _database;

    public MongoDbContext(IMongoDatabase database)
    {
        _database = database;
    }

    public IMongoCollection<Author> Authors => GetCollection<Author>(nameof(Authors));

    private IMongoCollection<TEntity> GetCollection<TEntity>(string name = "")
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(TEntity).Name + "s";

        return _database.GetCollection<TEntity>(name);
    }
}
