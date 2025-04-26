using PokyBack.Shared.Core.Entities;

namespace PokyBack.Responses;

public class RoomResponse
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedOn { get; set; }
    public string CreatedByUuid { get; set; }
    public bool IsRevealed { get; set; }
    public string Topic { get; set; }
    public IEnumerable<LogResponse> Logs { get; set; }

    public RoomResponse LoadFromEntity(Room entity)
    {
        if (entity is null)
            return this;
        
        Id = entity.Id;
        Code = entity.Code;
        CreatedBy = entity.CreatedBy;
        CreatedAt = entity.CreatedAt;
        DeletedOn = entity.DeletedOn;
        CreatedByUuid = entity.CreatedByUuid;
        IsRevealed = entity.IsRevealed;
        Topic = entity.Topic;

        Logs = entity.Logs.Select<Log, LogResponse>(x => new LogResponse().LoadFromEntity(x));

        return this;
    }
}