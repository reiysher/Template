using Application.Abstractions.Messaging.Commands;

namespace Application.Features.Notes.Commands.Create;

public sealed record CreateNoteCommand(Guid AuthorId, string Title, string Content) : ICommand;
