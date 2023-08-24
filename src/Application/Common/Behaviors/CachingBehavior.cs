using Application.Abstractions.Messaging.Queries;
using MediatR;

namespace Application.Common.Behaviors;

// todo: taskomask
internal class CachingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICacheableQuery<TResponse>
{
    public async Task<TResponse> Handle(TRequest query,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // todo: check global cache settings additionaly
        if (!query.EnableCache)
            return await next();

        // todo: get cache key and try get value then return value
        // todo: if value is not cached, run query, get value
        var response = await next();

        // todo: and set cache

        return response;
    }
}
