using Domain.Authors;
using Infrastructure.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Initialization;

internal static class MongoDbIntializer
{
    internal static async Task InitializeMongoDb(this IServiceProvider serviceProvider)
    {
        await serviceProvider.CreateIndexes();
    }

    private static async Task CreateIndexes(this IServiceProvider serviceProvider)
    {
        using var serviceScope = serviceProvider.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<MongoDbContext>();

        // example
        //await dbContext.Authors.Indexes.CreateOneAsync(
        //    new CreateIndexModel<Author>(Builders<Author>.IndexKeys.Ascending(a => a.BirthDay),
        //    new CreateIndexOptions { Name = nameof(Author.BirthDay), Unique = false }));
    }
}
