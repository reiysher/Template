using MediatR;

namespace Application.Abstractions.Messaging.Queries;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
