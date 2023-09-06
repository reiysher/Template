using Application.Abstractions.Persistence;
using Domain.Authors.Repositories;
using Domain.Notes.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using Persistence.Contexts;
using Persistence.Repositories.Authors;
using Persistence.Repositories.Notes;
using Persistence.Settings;
using Persistence.Initialization;
using Persistence.EventSourcing;

namespace Persistence;

public static class Configure
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<DatabaseSettings>()
            .BindConfiguration(DatabaseSettings.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();


        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            var settings = serviceProvider
                .GetRequiredService<IOptions<DatabaseSettings>>().Value;

            options.UseNpgsql(settings.PostgreSql.ConnectionString, builder =>
            {
                builder.CommandTimeout(settings.PostgreSql.CommandTimeoutInSeconds);
                builder.EnableRetryOnFailure();
                builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                builder.MigrationsHistoryTable(DbConstants.MigrationsHistory);
            });

            options.UseSnakeCaseNamingConvention();
        });

        // todo: scrutor
        services.AddScoped<INoteRepository, NoteRepository>();
        //services.AddScoped<IAuthorRepository, AuthorRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();


        // MongoDb
        var mongoSettings = services.BuildServiceProvider().GetRequiredService<IOptions<DatabaseSettings>>().Value.MongoDb;

        MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(mongoSettings.ConnectionString));
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        services.AddSingleton<IMongoDatabase>(new MongoClient(settings).GetDatabase(mongoSettings.DatabaseName));

        services.AddScoped<MongoDbContext>();
        services.AddScoped<IAuthorRepository, MongoDbAuthorRepository>();

        // Event Sourcing

        services.AddScoped<IEventStore, EventStore>();

        return services;
    }


    public static async Task InitializeDatabase(
        this IServiceProvider services,
        CancellationToken cancellationToken = default)
    {
        using var scope = services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await dbContext.Database.EnsureDeletedAsync(cancellationToken);
        await dbContext.Database.EnsureCreatedAsync(cancellationToken);

        await services.InitializeMongoDb();
    }
}
