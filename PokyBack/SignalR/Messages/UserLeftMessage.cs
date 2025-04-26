namespace PokyBack.SignalR.Messages;

public sealed class UserLeftMessage : WsMessage
{
    public UserLeftMessage()
    {
        Kind = EventKind.UserLeft;  
    }
}