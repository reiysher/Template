using Application.Abstractions.Messaging.Commands;

namespace Application.Features.Notes.Commands.DeleteByAuthorId;

public sealed record DeleteNotesByAuthorIdCommand(Guid AuthorId) : ICommand;
