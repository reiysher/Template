namespace Domain.Authors.Repositories;

public record AuthorFilter(IReadOnlyCollection<Guid> AuthorIds);
