using Infrastructure.BackgroundJobs;
using Infrastructure.Logging;
using Infrastructure.Messaging;
using Infrastructure.Notifications;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("IntegrationTests")] // todo: советуют убрать

namespace Infrastructure;

public static class Configure
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly consumersAssembly)
    {
        return services
            .RegisterMessaging(consumersAssembly)
            .AddDomainServices()
            .AddBackgroundJobs()
            .AddNotifications(configuration);
    }

    public static void EnableLogger(
        this WebApplicationBuilder builder)
    {
        builder.RegisterLogger();
    }

    public static IEndpointRouteBuilder MapHubs(this IEndpointRouteBuilder builder)
    {
        builder.MapNotifications();
        return builder;
    }
}
