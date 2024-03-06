using Application.Abstractions.Messaging.Queries;
using Application.Features.Authors.Models;
using Ardalis.GuardClauses;
using Domain.Authors;
using Domain.Authors.Guards;
using Domain.Authors.Repositories;

namespace Application.Features.Authors.Queries.GetById;

internal sealed class GetAuthorByIdQueryHandler : IQueryHandler<GetAuthorByIdQuery, AuthorDto>
{
    private readonly IAuthorRepository _repository;

    public GetAuthorByIdQueryHandler(IAuthorRepository repository)
    {
        _repository = repository;
    }

    public async Task<AuthorDto> Handle(GetAuthorByIdQuery query, CancellationToken cancellationToken)
    {
        var author = await _repository.GetById(query.AuthorId, cancellationToken);

        Guard.Against.AuthorNotFound(author, query.AuthorId);

        return new AuthorDto(author.Id, author.GetFullName(), author.BirthDay);
    }
}
