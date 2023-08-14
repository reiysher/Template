using MediatR;

namespace Application.Abstractions.Messaging.Commands;

public interface ICommand : IRequest
{
}

public interface ICommand<out TResponse> : ICommand, IRequest<TResponse>
{
}

// todo: Подумать над транзакцинностью комманд. Пример неявной транзакционности: https://youtu.be/sSIg3fpflI0
// todo: Почитат ьпро другие подходы