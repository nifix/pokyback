using PokyBack.Shared.Core.Entities;

namespace PokyBack.Responses;

public class LogResponse
{
    public int Id { get; set; }
    public string EventCode { get; set; } = null!;
    public string? UserUuid { get; set; }
    public string? Value { get; set; }
    public DateTime CreatedAt { get; set; }

    public LogResponse LoadFromEntity(Log entity)
    {
        Id = entity.Id;
        EventCode = entity.EventCode;
        UserUuid = entity.UserUuid;
        Value = entity.Value;
        CreatedAt = entity.CreatedAt;

        return this;
    }
}