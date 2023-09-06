using Application.Abstractions.Messaging.Commands;
using Application.Abstractions.Persistence;
using Ardalis.GuardClauses;
using Domain.Authors;
using Domain.Authors.Guards;
using Domain.Authors.Repositories;

namespace Application.Features.Authors.Commands.Delete;

internal sealed class DeleteAuthorCommandHandler : ICommandHandler<DeleteAuthorCommand>
{
    private readonly IAuthorRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAuthorCommandHandler(
        IAuthorRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteAuthorCommand command, CancellationToken cancellationToken)
    {
        var author = await _repository.GetById(command.AuthorId, cancellationToken);

        Guard.Against.AuthorNotFound(author, command.AuthorId);

        author.Delete();
        _repository.Delete(author);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
