using System.Text.Json.Serialization;

namespace PokyBack.SignalR.Messages;

public class UserJoinMessage : WsMessage
{
    [JsonPropertyName("username")]
    public string Username { get; set; }
    
    public UserJoinMessage()
    {
        Kind = EventKind.UserJoined;
    }
}