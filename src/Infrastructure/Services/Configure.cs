using Domain.Notes.Services;
using Infrastructure.Services.Notes;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services;

internal static class Configure
{
    internal static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddTransient<INoteService, NoteService>();

        return services;
    }
}
