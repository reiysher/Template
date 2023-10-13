using Application;
using Host.Endpoints;
using Host.Exceptions;
using Host.OpenApi;
using Host.Options;
using Infrastructure;
using Infrastructure.Logging;
using Persistence;
using Serilog;
using System.Reflection;

StaticLogger.EnsureInitialized();
Log.Information("Server Booting Up...");

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.EnableLogger();
    builder.ConfigureOptions();

    builder.Services.AddApplication(builder.Configuration);
    builder.Services.AddInfrastructure(builder.Configuration, Assembly.GetExecutingAssembly());
    builder.Services.AddPersistence(builder.Configuration);

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerPreConfigured();
    builder.Services.AddGlobalExceptionHandling();

    var app = builder.Build();

    app.UseGlobalExceptionHandling();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwaggerPreConfigured();
    }

    app.UseHttpsRedirection();

    app.MapEndpoints();
    app.MapHubs();

    await app.Services.InitializeDatabase();

    await app.RunAsync();
}
catch (Exception ex)
{
    StaticLogger.EnsureInitialized();
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    StaticLogger.EnsureInitialized();
    Log.Information("Server Shutting down...");
    Log.CloseAndFlush();
}

// todo: pagging result
// todo: swagger examples
// todo: polly для всех запросов во внешние системы
// todo: outbox для интеграции с внешними системами
// todo: sagas masstransit, подумать, можно реализовать какие-то процессы в монолите на сагах
// todo: specification - https://www.youtube.com/watch?v=rdY5ElleWKY
// todo: tests - https://www.youtube.com/watch?v=a6Qab5l-VLo&t=421s
// todo: ci/cd - https://youtu.be/QP0pi7xe24s
// todo: TransactionScope
// todo: CircuitBreaker => ClassifiedAds.Monolith
// todo: executingContext - scoped service with permissions, userId etc. cross cutting concerns https://habr.com/ru/articles/353258/
