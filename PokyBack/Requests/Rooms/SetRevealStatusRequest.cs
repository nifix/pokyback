using System.Text.Json.Serialization;

namespace PokyBack.Requests.Rooms;

public class SetRevealStatusRequest
{
    [JsonPropertyName("revealStatus")]
    public bool? RevealStatus { get; set; }
}