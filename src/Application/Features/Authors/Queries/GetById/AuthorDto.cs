namespace Application.Features.Authors.Queries.GetById;

public sealed record AuthorDto(Guid Id, string FullName, DateTime BirthDay);
