using System.Text.Json.Serialization;

namespace PokyBack.SignalR.Messages;

public sealed class TopicUpdatedMessage : WsMessage
{
    [JsonPropertyName("topic")]
    public string Topic { get; set; }

    public TopicUpdatedMessage()
    {
        Kind = EventKind.TopicUpdated;
    }
}