using Application.Abstractions.Messaging.Queries;
using Application.Features.Authors.Models;
using Domain.Authors.Repositories;

namespace Application.Features.Authors.Queries.Search;

internal class SearchAuthorHandler(IAuthorRepository repository)
    : IQueryHandler<SearchAuthorQuery, IReadOnlyCollection<AuthorDto>>
{
    public async Task<IReadOnlyCollection<AuthorDto>> Handle(
        SearchAuthorQuery query,
        CancellationToken cancellationToken)
    {
        var searchCriteria = new SearchAuthorCriteria(query.Filter, query.Sorter, query.Paging);
        var authors = await repository.Search(searchCriteria, cancellationToken);

        var authorDtos = authors
            .Select(author => new AuthorDto(author.Id, author.GetFullName(), author.BirthDay))
            .ToList();

        return authorDtos;
    }
}
