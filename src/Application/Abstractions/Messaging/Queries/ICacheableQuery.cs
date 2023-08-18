namespace Application.Abstractions.Messaging.Queries;

public interface ICacheableQuery<out TResponse> : IQuery<TResponse>
{
    bool EnableCache { get; }
}
