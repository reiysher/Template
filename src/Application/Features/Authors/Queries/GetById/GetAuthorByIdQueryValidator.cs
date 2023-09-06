using Application.Abstractions.Validation;
using FluentValidation;

namespace Application.Features.Authors.Queries.GetById;

internal sealed class GetAuthorByIdQueryValidator : CustomValidator<GetAuthorByIdQuery>
{
    public GetAuthorByIdQueryValidator()
    {
        RuleFor(query => query.AuthorId)
            .NotEmpty();
    }
}
