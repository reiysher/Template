using Application.Abstractions.Messaging.Queries;

namespace Application.Features.Authors.Queries.GetById;

public sealed record GetAuthorByIdQuery(Guid AuthorId) : IQuery<AuthorDto>;
