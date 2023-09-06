using Application.Abstractions.Messaging.Commands;
using Application.Abstractions.Persistence;
using Ardalis.GuardClauses;
using Domain.Authors;
using Domain.Authors.Guards;
using Domain.Authors.Repositories;
using Domain.Notes.Repositories;

namespace Application.Features.Notes.Commands.Create;

internal sealed class CreateNoteCommandHandler : ICommandHandler<CreateNoteCommand>
{
    private readonly INoteRepository _noteRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateNoteCommandHandler(
        INoteRepository repository,
        IAuthorRepository authorRepository,
        IUnitOfWork unitOfWork)
    {
        _noteRepository = repository;
        _authorRepository = authorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateNoteCommand command, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetById(command.AuthorId, cancellationToken);

        Guard.Against.AuthorNotFound(author, command.AuthorId);

        var note = author.WriteNote(command.Title, command.Content);
        _noteRepository.Add(note);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
