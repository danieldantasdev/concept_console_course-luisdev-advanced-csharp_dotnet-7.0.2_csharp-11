namespace CSharpAdvanced.Oop.Notifications;

public interface INotificationService
{
    void SendNotification(string message);
    string Format(string message);
}