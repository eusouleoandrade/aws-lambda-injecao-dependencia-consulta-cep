using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ConsultaCep.Infrastructure.Notification.Interfaces;
using ConsultaCep.Infrastructure.Notification.Models;

namespace ConsultaCep.Infrastructure.Notification.Abstractions
{
    public abstract class Notifiable : INotifiable
    {
        private List<NotificationMessage> _errorNotificationResult;

        private List<NotificationMessage> _successNotificationResult;

        [NotMapped]
        [JsonIgnore]
        public bool HasErrorNotification => ErrorNotificationResult.Any();

        [NotMapped]
        [JsonIgnore]
        public bool HasSuccessNotification => SuccessNotificationResult.Any();

        [NotMapped]
        [JsonIgnore]
        public IReadOnlyList<NotificationMessage> ErrorNotificationResult => _errorNotificationResult;

        [NotMapped]
        [JsonIgnore]
        public IReadOnlyList<NotificationMessage> SuccessNotificationResult => _successNotificationResult;

        public Notifiable()
        {
            _errorNotificationResult = new List<NotificationMessage>();
            _successNotificationResult = new List<NotificationMessage>();
        }

        protected void AddErrorNotification(NotificationMessage errorNotificationMessage) 
            => _errorNotificationResult.Add(errorNotificationMessage);

        protected void AddErrorNotification(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
                _errorNotificationResult.Add(new NotificationMessage(message));
        }

        protected void AddErrorNotification(IList<string> messages)
        {
            if (messages.Any())
                messages.ToList().ForEach(f => AddErrorNotification(f));
        }
        
        protected void AddErrorNotification(IEnumerable<NotificationMessage> messages)
        {
            if(messages.Any())
                messages.ToList().ForEach(f => AddErrorNotification(f));
        }

        protected void AddSuccessNotification(NotificationMessage successNotificationMessage) 
            => _successNotificationResult.Add(successNotificationMessage);
    }
}