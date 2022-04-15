using ConsultaCep.Infrastructure.Notification.Models;

namespace ConsultaCep.Infrastructure.Notification.Interfaces
{
    public interface INotifiable
    {
        bool HasErrorNotification { get; }

        bool HasSuccessNotification { get; }

        IReadOnlyList<NotificationMessage> ErrorNotificationResult { get; }

        IReadOnlyList<NotificationMessage> SuccessNotificationResult { get; }
    }
}