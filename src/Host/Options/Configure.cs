namespace Host.Options;

internal static class Configure
{
    internal static void ConfigureOptions(this WebApplicationBuilder builder)
    {
        builder.ConfigureWebServer();
        builder.ConfigureHeaders();
        builder.ConfigureJsonSerializer();
        builder.ConfigureRouting();
    }
}