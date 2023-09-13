using Application.Abstractions.Messaging.Commands;
using Application.Abstractions.Persistence;
using Domain.Authors;
using Domain.Notes.Services;

namespace Application.Features.Notes.Commands.DeleteByAuthorId;

internal sealed class DeleteNotesByAuthorIdCommandHandler : ICommandHandler<DeleteNotesByAuthorIdCommand>
{
    private readonly INoteService _noteService;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteNotesByAuthorIdCommandHandler(
        INoteService noteService,
        IUnitOfWork unitOfWork)
    {
        _noteService = noteService;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteNotesByAuthorIdCommand command, CancellationToken cancellationToken)
    {
        await _noteService.DeleteByAuthodId(command.AuthorId, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}
