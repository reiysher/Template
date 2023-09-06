using Application.Abstractions.Validation;
using FluentValidation;

namespace Application.Features.Notes.Commands.DeleteByAuthorId;

internal sealed class DeleteNotesByAuthorIdCommandValidator : CustomValidator<DeleteNotesByAuthorIdCommand>
{
    public DeleteNotesByAuthorIdCommandValidator()
    {
        RuleFor(command => command.AuthorId)
            .NotEmpty();
    }
}
