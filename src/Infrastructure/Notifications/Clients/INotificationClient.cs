namespace Infrastructure.Notifications.Clients;

public interface INotificationClient
{
    Task Notifications(string message);
}
