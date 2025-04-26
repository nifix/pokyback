using Microsoft.EntityFrameworkCore;
using PokyBack.Rooms.Core.Abstractions;
using PokyBack.Shared.Infrastructure.Persistence;

namespace PokyBack.Rooms.Infrastructure.Repositories;

public class RoomUserRepository(AppDbContext context) : IRoomUserRepository
{
    public async Task<bool> SetUserPickAsync(Guid roomCode, Guid uuid, int pickedCard, CancellationToken cancellationToken = default)
    {
        var roomUser = await context.RoomUsers.FirstOrDefaultAsync(s => s.RoomCode == roomCode.ToString() && s.Uuid == uuid, cancellationToken);
        if (roomUser is null)
            return false;
        
        roomUser.SetPick(pickedCard);
        await context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}