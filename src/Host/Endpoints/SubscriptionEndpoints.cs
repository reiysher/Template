using Application.Features.Subscriptions.Commands.Create;
using Application.Features.Subscriptions.Commands.Renew;
using Application.Features.Subscriptions.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Host.Endpoints;

internal static class SubscriptionEndpoints
{
    public static RouteGroupBuilder MapSubscriptionEndpoints(this RouteGroupBuilder group)
    {
        group.WithTags("Subscriptions");

        group.MapPost("", async (
            [FromBody] CreateSubscriptionCommand request, // todo: использовать запрос вместо команды
            [FromServices] ISender sender,
            CancellationToken cancellationToken) =>
        {
            //var command = new CreateAuthorCommand(CreateSubscriptionCommand.FirstName, request.BirthDay);
            await sender.Send(request, cancellationToken);

            return Results.Ok();
        })
            .Accepts<CreateSubscriptionCommand>(MediaTypeNames.Application.Json)
            .AllowAnonymous();

        group.MapPut("renew", async (
            [FromBody] RenewSubscriptionCommand request, // todo: использовать запрос вместо команды
            [FromServices] ISender sender,
            CancellationToken cancellationToken) =>
        {
            //var command = new CreateAuthorCommand(CreateSubscriptionCommand.FirstName, request.BirthDay);
            await sender.Send(request, cancellationToken);

            return Results.Ok();
        })
            .Accepts<RenewSubscriptionCommand>(MediaTypeNames.Application.Json)
            .AllowAnonymous();

        group.MapGet("{subscriptionId}", async (
            [FromRoute] Guid subscriptionId,
            [FromServices] ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetSubscriptionQuery(subscriptionId);
            var result = await sender.Send(query, cancellationToken);

            return Results.Ok(result);
        })
            .AllowAnonymous();

        return group;
    }
}
