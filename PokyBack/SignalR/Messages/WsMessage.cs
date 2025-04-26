using System.Text.Json.Serialization;

namespace PokyBack.SignalR.Messages;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "kind", IgnoreUnrecognizedTypeDiscriminators = true)]
[JsonDerivedType(typeof(UserJoinMessage), typeDiscriminator: EventKind.UserJoined)]
[JsonDerivedType(typeof(UserVotedMessage), typeDiscriminator: EventKind.UserVoted)]
[JsonDerivedType(typeof(LogMessage), typeDiscriminator: EventKind.LogMessage)]
[JsonDerivedType(typeof(TopicUpdatedMessage), typeDiscriminator: EventKind.TopicUpdated)]
[JsonDerivedType(typeof(ResetVotesMessage), typeDiscriminator: EventKind.ResetVotes)]
[JsonDerivedType(typeof(RevealVotesMessage), typeDiscriminator: EventKind.RevealVotes)]
[JsonDerivedType(typeof(RemoveUserMessage), typeDiscriminator: EventKind.UserRemoved)]
[JsonDerivedType(typeof(UserLeftMessage), typeDiscriminator: EventKind.UserLeft)]
public abstract class WsMessage
{
    [JsonPropertyName("kind")]
    public string Kind { get; set; }
    
    [JsonPropertyName("uuid")]
    public string Uuid { get; set; }
    
    [JsonPropertyName("roomId")]
    public string RoomId { get; set; }
}



