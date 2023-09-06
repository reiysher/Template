using Application.Features.Notes.Commands.DeleteByAuthorId;
using MassTransit;
using MediatR;
using Messaging.IntegrationEvents.Abstractions;

namespace Host.Consumers.Notes;

public sealed class AuthorDeletedConsumer : IConsumer<IAuthorDeletedIntegrationEvent>
{
    private readonly ISender _sender;

    public AuthorDeletedConsumer(ISender sender)
    {
        _sender = sender;
    }

    public Task Consume(ConsumeContext<IAuthorDeletedIntegrationEvent> context)
    {
        var integrationEvent = context.Message;
        var cancellationToken = context.CancellationToken;

        var command = new DeleteNotesByAuthorIdCommand(integrationEvent.AuthorId);

        return _sender.Send(command, cancellationToken);
    }
}
