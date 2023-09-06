namespace Host.Options;

internal static class RoutingOptions
{
    internal static void ConfigureRouting(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<RouteOptions>(options =>
        {
            options.AppendTrailingSlash = true;
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });
    }
}
