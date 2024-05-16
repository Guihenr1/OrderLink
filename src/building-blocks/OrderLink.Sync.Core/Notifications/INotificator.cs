using OrderLink.Sync.Core.Notifications;

namespace OrderLink.Sync.Core.Notifications;

public interface INotificator
{
    bool HasNotification();
    List<Notification> GetNotifications();
    void Handle(Notification notificacao);
    void CleanNotifications();
}