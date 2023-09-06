using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Host.Options;

internal static class WebServerOptions
{
    internal static void ConfigureWebServer(this WebApplicationBuilder builder)
    {
        builder.WebHost.ConfigureKestrel((context, serverOptions) =>
        {
            serverOptions.ListenLocalhost(8000, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http1;
            });

            serverOptions.ListenLocalhost(8001, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http1 |HttpProtocols.Http2 | HttpProtocols.Http3;
                listenOptions.UseHttps();
            });
        });
    }
}
