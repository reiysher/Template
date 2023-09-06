namespace Application.Features.Notes.Queries.GetById;

public sealed record NoteDto(Guid Id, string Title, string Content);
