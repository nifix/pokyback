using System.Text.Json;

namespace PokyBack.Shared.Core.Entities;

public class EntityBase
{
    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();
    
    public void AddLog(string code, string roomCode, object? value = null, string? userUuid = null)
    {
        Logs.Add(new Log
        {
            EventCode = code,
            RoomCode = roomCode,
            UserUuid = userUuid,
            Value = value is not null ? JsonSerializer.Serialize(value) : null
        });
    }
}