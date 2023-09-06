using Microsoft.AspNetCore.HttpOverrides;

namespace Host.Options;

internal static class HeadersOptions
{
    internal static void ConfigureHeaders(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        });
    }
}
