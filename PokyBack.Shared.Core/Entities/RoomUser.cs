namespace PokyBack.Shared.Core.Entities;

public class RoomUser
{
    public int Id { get; set; }
    public Guid Uuid { get; set; }
    public string RoomCode { get; set; } = null!;
    public DateTime JoinedAt { get; set; }
    public string Username { get; set; } = null!;
    public int? CurrentPick { get; set; }

    public virtual Room Room { get; set; } = null!;

    /// <summary>
    /// Sets the current pick for the room user.
    /// </summary>
    /// <param name="pick">The card number to set as the current pick.</param>
    public void SetPick(int pick)
    {
        CurrentPick = pick;
    }
}
