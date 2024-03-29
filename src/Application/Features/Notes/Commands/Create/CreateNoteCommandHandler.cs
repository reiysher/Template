﻿using Application.Abstractions.Messaging.Commands;
using Application.Abstractions.Persistence;
using Ardalis.GuardClauses;
using Domain.Authors.Guards;
using Domain.Authors.Repositories;
using Domain.Notes.Repositories;

namespace Application.Features.Notes.Commands.Create;

internal class CreateNoteCommandHandler(
    INoteRepository repository,
    IAuthorRepository authorRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateNoteCommand>
{
    private readonly INoteRepository _noteRepository = repository;
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Handle(CreateNoteCommand command, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetById(command.AuthorId, cancellationToken);

        Guard.Against.AuthorNotFound(author, command.AuthorId);

        var note = author.WriteNote(command.Title, command.Content);
        _noteRepository.Insert(note);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}
