using Infrastructure.BackgroundJobs.Extensions;
using Infrastructure.BackgroundJobs.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Infrastructure.BackgroundJobs;

internal static class Configure
{
    internal static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
    {
        services.AddQuartz(options =>
        {
            // todo: move to config
            options.ConfigureJob<LoggingJob>("*/5 * * * * ?");
        });

        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });

        return services;
    }
}
