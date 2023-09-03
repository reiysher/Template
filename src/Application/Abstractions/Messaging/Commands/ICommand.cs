using MediatR;

namespace Application.Abstractions.Messaging.Commands;

public interface ICommand : IRequest
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}

// todo: Подумать над транзакцинностью комманд. Пример неявной транзакционности: https://youtu.be/sSIg3fpflI0
// todo: Почитать про другие подходы