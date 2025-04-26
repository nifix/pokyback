namespace PokyBack.SignalR.Messages;

public sealed class RemoveUserMessage : WsMessage
{
    public RemoveUserMessage()
    {
        Kind = EventKind.UserRemoved;   
    }
}