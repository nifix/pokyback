namespace PokyBack.SignalR.Messages;

public sealed class RevealVotesMessage : WsMessage
{
    public RevealVotesMessage()
    {
        Kind = EventKind.RevealVotes;
    }  
}