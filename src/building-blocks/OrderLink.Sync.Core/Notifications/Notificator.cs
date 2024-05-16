using Microsoft.Extensions.Logging;

namespace OrderLink.Sync.Core.Notifications;

public class Notificator : INotificator
{
    private readonly List<Notification> _notifications;
    private readonly ILogger<Notification> _logger;

    public Notificator(ILogger<Notification> logger)
    {
        _notifications = new List<Notification>();
        _logger = logger;
    }

    public List<Notification> GetNotifications()
    {
        return _notifications;
    }
    
    public void Handle(Notification notificacao)
    {
        _notifications.Add(notificacao);
        _logger.LogError(notificacao.Mensagem);
    }

    public bool HasNotification()
    {
        return _notifications.Any();
    }

    public void CleanNotifications()
    {
        _notifications.Clear();
    }
}