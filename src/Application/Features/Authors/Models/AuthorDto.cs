namespace Application.Features.Authors.Models;

public sealed record AuthorDto(Guid Id, string FullName, DateTime BirthDay);
