using Application.Abstractions.Validation;
using FluentValidation;

namespace Application.Features.Authors.Commands.Create;

internal sealed class CreateAuthorCommandValidator : CustomValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(command => command.FirstName)
            .NotEmpty();

        RuleFor(command => command.BirthDay)
            .NotEmpty();
    }
}
