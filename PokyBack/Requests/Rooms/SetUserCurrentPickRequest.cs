using System.Text.Json.Serialization;

namespace PokyBack.Requests.Rooms;

public class SetUserCurrentPickRequest
{
    [JsonPropertyName("currentPick")]
    public int CurrentPickedCard { get; set; }
}