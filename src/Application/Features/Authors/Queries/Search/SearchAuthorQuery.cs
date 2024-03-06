using Application.Abstractions.Messaging.Queries;
using Application.Features.Authors.Models;
using Domain.Authors.Repositories;

namespace Application.Features.Authors.Queries.Search;

public record SearchAuthorQuery(AuthorFilter Filter, AuthorSorter Sorter, Paging Paging)
    : IQuery<IReadOnlyCollection<AuthorDto>>;
