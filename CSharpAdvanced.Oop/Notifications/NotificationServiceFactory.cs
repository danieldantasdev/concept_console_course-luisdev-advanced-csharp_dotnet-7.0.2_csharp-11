using CSharpAdvanced.Oop.Enums;

namespace CSharpAdvanced.Oop.Notifications;

public static class NotificationServiceFactory
{
    public static INotificationService CreateNotificationService(NotificationEnum @enum)
    {
        switch (@enum)
        {
            case NotificationEnum.Push:
                return new PushNotificationService();
            case NotificationEnum.Sms:
                return new SmsService();
            case NotificationEnum.Email:
                return new EmailService();
            default:
                throw new ArgumentException("Invalid notification @enum");
        }
    }
}