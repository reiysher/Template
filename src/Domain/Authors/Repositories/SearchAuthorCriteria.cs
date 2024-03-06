namespace Domain.Authors.Repositories;

public record SearchAuthorCriteria(AuthorFilter Filter, AuthorSorter Sorter, Paging Paging);
