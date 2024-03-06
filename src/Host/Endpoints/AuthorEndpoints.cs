using Application.Features.Authors.Commands.Create;
using Application.Features.Authors.Commands.Delete;
using Application.Features.Authors.Models;
using Application.Features.Authors.Queries.GetById;
using Application.Features.Authors.Queries.Search;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Host.Endpoints;

internal static class AuthorEndpoints
{
    public static RouteGroupBuilder MapAuthorEndpoints(this RouteGroupBuilder group)
    {
        group.WithTags("Authors");

        group.MapPost("search", Search)
            .Produces<IReadOnlyCollection<AuthorDto>>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)
            .Accepts<SearchAuthorRequest>(MediaTypeNames.Application.Json)
            .AllowAnonymous();

        group.MapGet("{authorId:guid}", Get)
            .Produces<AuthorDto?>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)
            .AllowAnonymous();

        group.MapPost("", Create)
            .Accepts<CreateAuthorRequest>(MediaTypeNames.Application.Json)
            .AllowAnonymous();

        group.MapDelete("{authorId:guid}", Delete)
            .AllowAnonymous();

        return group;
    }

    public static async Task<IResult> Search(
        [FromBody] SearchAuthorRequest request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new SearchAuthorQuery(request.Filter, request.Sorter, request.Paging);
        var authorDtos = await sender.Send(query, cancellationToken);
        return Results.Ok(authorDtos);
    }

    public static async Task<IResult> Get(
        [FromRoute] Guid authorId,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new GetAuthorByIdQuery(authorId);
        var authorDto = await sender.Send(query, cancellationToken);
        return Results.Ok(authorDto);
    }

    public static async Task<IResult> Create(
        [FromBody] CreateAuthorRequest request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new CreateAuthorCommand(request.FirstName, request.BirthDay);
        var result = await sender.Send(command, cancellationToken);

        return Results.Ok(result);
    }

    public static async Task<IResult> Delete(
        [FromRoute] Guid authorId,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new DeleteAuthorCommand(authorId);
        await sender.Send(command, cancellationToken);
        return Results.Ok();
    }
}
