﻿namespace OrderLink.Sync.Core.Notifications;

public class Notification
{
    public Notification(string mensagem)
    {
        Mensagem = mensagem;
    }

    public string Mensagem { get; }
}