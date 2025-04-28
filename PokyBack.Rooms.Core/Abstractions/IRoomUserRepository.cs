using PokyBack.Shared.Core.Entities;

namespace PokyBack.Rooms.Core.Abstractions;

public interface IRoomUserRepository
{
    /// <summary>
    /// Sets the user's picked card for a given room.
    /// </summary>
    /// <param name="roomCode">The ID of the room.</param>
    /// <param name="uuid">The unique identifier of the user.</param>
    /// <param name="pickedCard">The card picked by the user.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <return>True if the operation was successful; otherwise, false.</return>
    public Task<bool> SetUserPickAsync(Guid roomCode, Guid uuid, int pickedCard, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the list of users in a specified room.
    /// </summary>
    /// <param name="roomCode">The unique identifier of the room.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <return>A task that represents the asynchronous operation. The task result contains a list of room users.</return>
    public Task<List<RoomUser>> GetRoomUsersAsync(Guid roomCode, CancellationToken cancellationToken = default);
}