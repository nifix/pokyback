using System.Text.Json.Serialization;

namespace PokyBack.SignalR.Messages;

public sealed class LogMessage : WsMessage
{
    [JsonPropertyName("message")]
    public string? Message { get; set; }
    
    public LogMessage()
    {
        Kind = EventKind.LogMessage;
    }
}