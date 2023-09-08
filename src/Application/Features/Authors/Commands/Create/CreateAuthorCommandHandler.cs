﻿using Application.Abstractions.Messaging.Commands;
using Application.Abstractions.Persistence;
using Domain.Authors;
using Domain.Authors.Repositories;

namespace Application.Features.Authors.Commands.Create;

internal sealed class CreateAuthorCommandHandler : ICommandHandler<CreateAuthorCommand, Guid>
{
    private readonly IAuthorRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAuthorCommandHandler(
        IAuthorRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateAuthorCommand command, CancellationToken cancellationToken)
    {
        var author = Author.Create(command.FirstName, command.BirthDay);

        _repository.Add(author);

        await _unitOfWork.Commit(cancellationToken);

        return author.Id;
    }
}
