using Microsoft.Extensions.Logging;
using Quartz;

namespace Infrastructure.BackgroundJobs.Jobs;

[DisallowConcurrentExecution]
internal class LoggingJob : IJob
{
    private readonly ILogger<LoggingJob> _logger;

    public LoggingJob(ILogger<LoggingJob> logger)
    {
        _logger = logger;
    }

    public Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("{UtcNow}", DateTimeOffset.UtcNow);

        return Task.CompletedTask;
    }
}
