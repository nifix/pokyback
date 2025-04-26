using System.Text.Json.Serialization;

namespace PokyBack.Requests.Rooms;

public class AddUserToRoomRequest
{
    [JsonPropertyName("uuid")]
    public Guid Uuid { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }
}