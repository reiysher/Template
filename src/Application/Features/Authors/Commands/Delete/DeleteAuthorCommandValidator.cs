using Application.Abstractions.Validation;
using FluentValidation;

namespace Application.Features.Authors.Commands.Delete;

internal sealed class DeleteAuthorCommandValidator : CustomValidator<DeleteAuthorCommand>
{
    public DeleteAuthorCommandValidator()
    {
        RuleFor(command => command.AuthorId)
            .NotEmpty();
    }
}
