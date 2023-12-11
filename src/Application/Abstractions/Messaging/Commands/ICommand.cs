using MediatR;

namespace Application.Abstractions.Messaging.Commands;

public interface ICommand : IRequest
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}