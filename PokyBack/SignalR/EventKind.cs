namespace PokyBack.SignalR;

public static class EventKind
{
    public const string UserJoined = "user-joined";
    public const string UserVoted = "user-voted";
    public const string ResetVotes = "reset-votes";
    public const string RevealVotes = "reveal-votes";
    public const string LogMessage = "log-message";
    public const string UserRemoved = "user-removed";
    public const string UserLeft = "user-left";
    public const string TopicUpdated  = "topic-updated";
}