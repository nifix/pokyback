namespace PokyBack.Shared.Core.Entities;

#nullable disable

public class Room : EntityBase
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedOn { get; set; }
    public string CreatedByUuid { get; set; }
    public bool IsRevealed { get; set; }
    public string Topic { get; set; }

    // Navigation Properties
    public virtual ICollection<RoomUser> RoomUsers { get; set; } = new List<RoomUser>();

    public void AddUser(Guid uuid, string username)
    {
        RoomUsers.Add(new RoomUser
        {
            RoomCode = Code,
            Username = username,
            Uuid = uuid,
            JoinedAt = DateTime.Now,
        });
        
        AddLog("user_joined", Code, userUuid: uuid.ToString());
    }    
    
    /// <summary>
    /// Sets the reveal status of the room.
    /// </summary>
    /// <param name="revealStatus">The new reveal status for the room.</param>
    public void SetRevealStatus(bool revealStatus)
    {
        IsRevealed = revealStatus;
        AddLog("room_reveal_status_changed", Code, revealStatus);
    }
    
    /// <summary>
    /// Sets the status of the room.
    /// </summary>
    /// <param name="topic">The new reveal status for the room.</param>
    /// <param name="uuid">Uuid of the one who requested the change.</param>
    public void SetTopic(Guid uuid, string topic)
    {
        Topic = topic;
        AddLog("room_topic_changed", Code, topic, userUuid: uuid.ToString());
    }

    /// <summary>
    /// Initializes a new room with a unique code, creation timestamp, and creator information.
    /// </summary>
    /// <param name="username">The username of the room creator.</param>
    /// <param name="uuid">The UUID of the room creator.</param>
    /// <returns>The initialized <see cref="Room"/> object.</returns>
    public Room InitializeNewRoom(string username, Guid uuid)
    {
        Code = Guid.NewGuid().ToString();
        CreatedAt = DateTime.Now;
        CreatedBy = username;
        CreatedByUuid = uuid.ToString();
        
        // Add Log
        AddLog("room_created", Code, userUuid: CreatedByUuid);

        return this;
    }
}