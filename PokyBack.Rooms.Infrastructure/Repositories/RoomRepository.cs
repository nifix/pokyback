using Microsoft.EntityFrameworkCore;
using PokyBack.Rooms.Core.Abstractions;
using PokyBack.Shared.Core.Entities;
using PokyBack.Shared.Infrastructure.Persistence;

namespace PokyBack.Rooms.Infrastructure.Repositories;

public class RoomRepository(AppDbContext context) : IRoomRepository
{
    public async Task<List<Room>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context
            .Rooms
            .ToListAsync(cancellationToken);
    }

    public async Task<Room?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context
            .Rooms
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken) ?? null;
    }
    
    public async Task<Room?> GetByCodeAsync(Guid code, CancellationToken cancellationToken = default)
    {
        return await context
            .Rooms
            .Include(s => s.Logs)
            .FirstOrDefaultAsync(s => s.Code == code.ToString(), cancellationToken) ?? null;
    }

    public async Task<Room?> CreateRoomAsync(string username, Guid uuid, CancellationToken cancellationToken = default)
    {
        // Initialize new room
        var toCreate = new Room().InitializeNewRoom(username, uuid);
        
        // Save changes
        var room = await context.Rooms.AddAsync(toCreate, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        
        return room.Entity;
    }

    public async Task<bool> SetRevealStatusAsync(Guid roomCode, bool revealStatus, CancellationToken cancellationToken = default)
    {
        var room = await context.Rooms.FirstOrDefaultAsync(r => r.Code == roomCode.ToString(), cancellationToken);
        if (room is null) 
            return false;
        
        room.SetRevealStatus(revealStatus);
        await context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
    
    public async Task<bool> SetTopicAsync(Guid roomCode, Guid uuid, string topic, CancellationToken cancellationToken = default)
    {
        var room = await context.Rooms.FirstOrDefaultAsync(r => r.Code == roomCode.ToString(), cancellationToken);
        if (room is null) 
            return false;
        
        room.SetTopic(uuid, topic);
        await context.SaveChangesAsync(cancellationToken);
        
        return true;
    }

    public async Task<bool> AddUserAsync(Guid roomCode, Guid uuid, string username,
        CancellationToken cancellationToken = default)
    {
        var room = await context.Rooms.FirstOrDefaultAsync(r => r.Code == roomCode.ToString(), cancellationToken);
        if (room is null)
            return false;

        room.AddUser(uuid, username);
        await context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> RemoveUserAsync(Guid roomCode, Guid uuid, CancellationToken cancellationToken = default)
    {
        var room = await context.Rooms.FirstOrDefaultAsync(r => r.Code == roomCode.ToString(), cancellationToken);
        if (room is null)
            return false;

        room.RemoveUser(uuid);
        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}