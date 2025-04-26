using System.Text.Json;
using PokyBack.Shared.Core.Abstractions;
using PokyBack.Shared.Core.Entities;
using PokyBack.Shared.Infrastructure.Persistence;

namespace PokyBack.Shared.Infrastructure.Repositories;

public class LogRepository(AppDbContext context) : ILogRepository
{
    public async Task AddLog(string code, string roomCode, object? value = null, string? userUuid = null)
    {
        await context.Logs.AddAsync(new Log
        {
            RoomCode = roomCode,
            CreatedAt = DateTime.Now,
            UserUuid = userUuid,
            EventCode = code,
            Value = JsonSerializer.Serialize(value)
        });
        
        await context.SaveChangesAsync();
    }
}