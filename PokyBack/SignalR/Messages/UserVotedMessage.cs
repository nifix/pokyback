using System.Text.Json.Serialization;

namespace PokyBack.SignalR.Messages;

public sealed class UserVotedMessage : WsMessage
{
    [JsonPropertyName("cardId")]
    public int? CardId { get; set; }

    public UserVotedMessage()
    {
        Kind = EventKind.UserVoted;
    }
}