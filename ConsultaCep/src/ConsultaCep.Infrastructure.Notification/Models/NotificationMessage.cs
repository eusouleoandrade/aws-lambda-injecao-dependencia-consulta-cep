namespace ConsultaCep.Infrastructure.Notification.Models
{
    public class NotificationMessage
    {
        public string Message { get; set; }

        public NotificationMessage(string message)
        {
            Message = message;
        }
    }
}