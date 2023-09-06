using Application.Abstractions.Messaging.Commands;

namespace Application.Features.Authors.Commands.Delete;

public sealed record DeleteAuthorCommand(Guid AuthorId) : ICommand;
