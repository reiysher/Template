using Application.Abstractions.Validation;
using FluentValidation;

namespace Application.Features.Notes.Queries.GetById;

internal sealed class GetNoteByIdQueryValidator : CustomValidator<GetNoteByIdQuery>
{
    public GetNoteByIdQueryValidator()
    {
        RuleFor(query => query.NoteId)
            .NotEmpty();
    }
}
