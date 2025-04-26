using System.Text.Json.Serialization;

namespace PokyBack.Requests.Rooms;

public class SetTopicRequest
{
    [JsonPropertyName("uuid")]
    public Guid Uuid { get; set; }
    
    [JsonPropertyName("topic")]
    public string Topic { get; set; }
}