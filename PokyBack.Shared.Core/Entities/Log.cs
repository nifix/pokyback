namespace PokyBack.Shared.Core.Entities;

public class Log
{
    public int Id { get; set; }
    public string EventCode { get; set; } = null!;
    public string? RoomCode { get; set; }
    public string? UserUuid { get; set; }
    public string? Value { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public virtual Room? Room { get; set; }
}