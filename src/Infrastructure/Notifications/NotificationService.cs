using Application.Features.Notifications.Services;
using Infrastructure.Notifications.Clients;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Notifications;

/// <inheritdoc cref="INotificationService"/>
internal class NotificationService : INotificationService
{
    private readonly IHubContext<NotificationHub, INotificationClient> _hub;

    public NotificationService(IHubContext<NotificationHub, INotificationClient> hub)
    {
        _hub = hub;
    }
}
