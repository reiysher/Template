using Quartz;

namespace Infrastructure.BackgroundJobs.Extensions;

internal static class QuartzExtensions
{
    public static IServiceCollectionQuartzConfigurator ConfigureJob<T>(
        this IServiceCollectionQuartzConfigurator options,
        string cronExpression)
        where T : IJob
    {
        var jobKey = JobKey.Create(nameof(T));

        options
            .AddJob<T>(builder => builder.WithIdentity(jobKey))
            .AddTrigger(trigger =>
                trigger
                    .ForJob(jobKey)
                    .WithCronSchedule(cronExpression));

        return options;
    }
}
