using Application.Abstractions.Messaging.Queries;

namespace Application.Features.Notes.Queries.GetById;

public sealed record GetNoteByIdQuery(Guid NoteId) : IQuery<NoteDto>;
