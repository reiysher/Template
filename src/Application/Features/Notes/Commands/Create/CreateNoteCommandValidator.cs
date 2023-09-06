using Application.Abstractions.Validation;
using FluentValidation;

namespace Application.Features.Notes.Commands.Create;

internal sealed class CreateNoteCommandValidator : CustomValidator<CreateNoteCommand>
{
	public CreateNoteCommandValidator()
    {
        RuleFor(command => command.AuthorId)
            .NotEmpty();

        RuleFor(command => command.Title)
			.NotEmpty();

        RuleFor(command => command.Content)
            .NotEmpty();
    }
}
