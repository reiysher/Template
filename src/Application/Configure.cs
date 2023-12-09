using Application.Common.Behaviors;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class Configure
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services
            .AddMediatR(options => options.RegisterServicesFromAssembly(assembly))
            .AddBehaviors();

        services
            .AddValidatorsFromAssembly(assembly);

        services
            .AddSingleton(TimeProvider.System);

        return services;
    }
}