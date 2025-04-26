namespace PokyBack.SignalR.Messages;

public sealed class ResetVotesMessage : WsMessage
{
    public ResetVotesMessage()
    {
        Kind = EventKind.ResetVotes;
    }   
}