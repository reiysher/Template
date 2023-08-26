using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviors;

internal class LogingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseRequest
{
    private readonly ILogger<TRequest> _logger;

    public LogingBehavior(ILogger<TRequest> logger) =>
        _logger = logger;

    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // todo: move into config "enable sensetive data logging" and logging only if enabled.
        _logger.LogInformation("Handling [{@Request}]", typeof(TRequest).Name);

        return await next();
    }
}
