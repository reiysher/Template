namespace Host.Endpoints;

public static class Configure
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapGroup("notes")
            .MapNoteEndpoints()
            .WithOpenApi();

        app.MapGroup("authors")
            .MapAuthorEndpoints()
            .WithOpenApi();

        app.MapGroup("subscriptions")
            .MapSubscriptionEndpoints()
            .WithOpenApi();
    }
}