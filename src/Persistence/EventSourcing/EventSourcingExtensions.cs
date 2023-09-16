using Domain.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Persistence.EventSourcing;

public static class EventSourcingExtensions
{
    public static IServiceCollection AddEventSourcing(
        this IServiceCollection services,
        Action<EventSourcingOptions>? options = null)
    {
        services.TryAddScoped<IEventStore, EventStore>();

        options?.Invoke(new EventSourcingOptions(services));

        return services;
    }
}

public class EventSourcingOptions
{
    private readonly IServiceCollection _services;

    public EventSourcingOptions(IServiceCollection services)
    {
        _services = services;
    }

    public void AddProjection<TProjectionSource, TProjection, TId>()
        where TProjectionSource : class, IProjectionSource
        where TProjection : class, IProjection<TId>
        where TId : notnull
    {
        _services.AddScoped<IProjectionSource, TProjectionSource>();
        _services.AddScoped<IProjectionStore<TProjection, TId>, ProjectionStore<TProjection, TId>>();
    }
}
