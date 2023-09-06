using Application.Abstractions.Messaging.Queries;
using Ardalis.GuardClauses;
using Domain.Notes;
using Domain.Notes.Exceptions;
using Domain.Notes.Guards;
using Domain.Notes.Repositories;

namespace Application.Features.Notes.Queries.GetById;

internal sealed class GetNoteByIdQueryHandler : IQueryHandler<GetNoteByIdQuery, NoteDto>
{
    private readonly INoteRepository _repository;

    public GetNoteByIdQueryHandler(INoteRepository repository)
    {
        _repository = repository;
    }

    public async Task<NoteDto> Handle(GetNoteByIdQuery query, CancellationToken cancellationToken)
    {
        var noteId = new NoteId(query.NoteId);
        var note = await _repository.GetById(noteId, cancellationToken);

        Guard.Against.NoteNotFound(note, noteId);

        return new NoteDto(note.Id.Value, note.Title, note.Content);
    }
}
