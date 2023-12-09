using Application.Abstractions.Messaging.DomainEvents;
using Infrastructure.Persistence.Contexts;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure.Messaging;

internal static class Configure
{
    public static IServiceCollection RegisterMessaging(this IServiceCollection services, Assembly consumersAssembly)
    {
        services.AddMassTransit(options =>
        {
            options.SetEndpointNameFormatter(new SnakeCaseEndpointNameFormatter('.', "template", false));
            options.AddConsumers(consumersAssembly);

            options.AddEntityFrameworkOutbox<ApplicationDbContext>(config =>
            {
                // outbox
                config.QueryDelay = TimeSpan.FromSeconds(5);
                config.UseBusOutbox();

                // inbox
                config.DuplicateDetectionWindow = TimeSpan.FromSeconds(30);
                config.DisableInboxCleanupService();

                config.UsePostgres();
            });

            options.UsingRabbitMq((context, config) =>
            {
                config.AutoStart = true;
                config.ConfigureEndpoints(context);
            });

            options.AddConfigureEndpointsCallback((context, name, config) =>
            {
                // for all endpoints
                config.UseMessageRetry(r => r.Incremental(5, TimeSpan.FromMilliseconds(100), TimeSpan.FromMilliseconds(100)));
                config.UseEntityFrameworkOutbox<ApplicationDbContext>(context);
            });
        });

        // todo: scrutor

        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        return services;
    }
}
