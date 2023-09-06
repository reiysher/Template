using Infrastructure.Notifications.Clients;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Notifications;

public class NotificationHub : Hub<INotificationClient>
{
    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }
}
