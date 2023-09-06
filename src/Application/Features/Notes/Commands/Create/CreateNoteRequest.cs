namespace Application.Features.Notes.Commands.Create;

// todo: move it
public sealed record CreateNoteRequest(Guid AuthorId, string Title, string Content);
