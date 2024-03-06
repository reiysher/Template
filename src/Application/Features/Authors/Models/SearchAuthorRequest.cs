using Domain.Authors.Repositories;

namespace Application.Features.Authors.Models;

public record SearchAuthorRequest(AuthorFilter Filter, AuthorSorter Sorter, Paging Paging);
