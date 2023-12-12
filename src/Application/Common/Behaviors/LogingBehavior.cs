using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviors;

internal class LogingBehavior<TRequest, TResponse>(
    ILogger<TRequest> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseRequest
{
    private readonly ILogger<TRequest> _logger = logger;

    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Handling [{@Request}]", typeof(TRequest).Name);

            var result = await next();

            _logger.LogInformation("[{@Request}] processed successful", typeof(TRequest).Name);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[{@Request}] processing failed", typeof(TRequest).Name);
            throw;
        }
    }
}
