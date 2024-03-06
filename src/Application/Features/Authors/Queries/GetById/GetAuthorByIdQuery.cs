using Application.Abstractions.Messaging.Queries;
using Application.Features.Authors.Models;

namespace Application.Features.Authors.Queries.GetById;

public sealed record GetAuthorByIdQuery(Guid AuthorId) : IQuery<AuthorDto>;
