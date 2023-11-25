using Application.Features.Notes.Commands.Create;
using Application.Features.Notes.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Host.Endpoints;

internal static class NoteEndpoints
{
    public static RouteGroupBuilder MapNoteEndpoints(this RouteGroupBuilder group)
    {
        group.WithTags("Notes");

        group.MapGet("{noteId:guid}", Get)
            .Produces<NoteDto?>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)
            .AllowAnonymous();

        group.MapPost("", Create)
            .Accepts<CreateNoteRequest>(MediaTypeNames.Application.Json)
            .AllowAnonymous();

        return group;
    }

    public static async Task<IResult> Get(
            [FromRoute] Guid noteId,
            [FromServices] ISender sender,
            CancellationToken cancellationToken)
    {
        var query = new GetNoteByIdQuery(noteId);
        var noteDto = await sender.Send(query, cancellationToken);
        return Results.Ok(noteDto);
    }

    public static async Task<IResult> Create(
            [FromBody] CreateNoteRequest request,
            [FromServices] ISender sender,
            CancellationToken cancellationToken)
    {
        var command = new CreateNoteCommand(request.AuthorId, request.Title, request.Content);
        await sender.Send(command, cancellationToken);

        // todo: return Created
        return Results.Ok();
    }
}
