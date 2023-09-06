using Application.Abstractions.Messaging.DomainEvents;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure.Messaging;

internal static class Configure
{
    public static IServiceCollection RegisterMessaging(this IServiceCollection services, Assembly consumersAssembly)
    {

        services.AddMassTransit(config =>
        {
            config.SetSnakeCaseEndpointNameFormatter();
            config.AddConsumers(consumersAssembly);

            config.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });

        // todo: scrutor

        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        return services;
    }
}
