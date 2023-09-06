using Application.Abstractions.Messaging.Commands;

namespace Application.Features.Authors.Commands.Create;

public sealed record CreateAuthorCommand(string FirstName, DateTime BirthDay) : ICommand<Guid>;
