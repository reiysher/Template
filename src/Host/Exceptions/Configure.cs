using Domain.Authors.Exceptions;
using Domain.Notes.Exceptions;
using Hellang.Middleware.ProblemDetails;

namespace Host.Exceptions;

internal static class Configure
{
    // todo: thonk about IExceptionHandler in .net 8
    internal static IServiceCollection AddGlobalExceptionHandling(this IServiceCollection services)
    {
        services.AddProblemDetails(config =>
        {
            config.IncludeExceptionDetails = (ctx, ex) =>
            {
                // Fetch services from HttpContext.RequestServices
                var env = ctx.RequestServices.GetRequiredService<IHostEnvironment>();
                return env.IsDevelopment() || env.IsStaging();
            };

            config.MapToStatusCode<NoteNotFoundException>(StatusCodes.Status404NotFound);
            config.MapToStatusCode<AuthorNotFoundException>(StatusCodes.Status404NotFound);
        });

        return services;
    }

    internal static void UseGlobalExceptionHandling(this WebApplication app)
    {
        app.UseProblemDetails();
    }
}
