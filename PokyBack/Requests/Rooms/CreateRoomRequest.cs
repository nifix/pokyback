using System.Text.Json.Serialization;

namespace PokyBack.Requests.Rooms;

public class CreateRoomRequest
{
    [JsonPropertyName("username")]
    public string Username { get; set; }
    
    [JsonPropertyName("uuid")]
    public Guid? Uuid { get; set; }
}