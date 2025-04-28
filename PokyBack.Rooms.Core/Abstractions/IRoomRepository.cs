using PokyBack.Shared.Core.Entities;

namespace PokyBack.Rooms.Core.Abstractions;

public interface IRoomRepository
{
    /// <summary>
    /// Retrieves a list of all Room entities asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of Room entities.</returns>
    public Task<List<Room>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a Room entity by its unique identifier asynchronously.
    /// </summary>
    /// <param name="request">The query request containing the unique identifier of the Room.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the Room entity if found; otherwise, null.</returns>
    public Task<Room?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a Room entity by its unique code asynchronously.
    /// </summary>
    /// <param name="code">The unique code of the Room.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the Room entity if found; otherwise, null.</returns>
    public Task<Room?> GetByCodeAsync(Guid code, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new Room entity asynchronously.
    /// </summary>
    /// <param name="username">The username of the user creating the Room.</param>
    /// <param name="uuid">The unique identifier of the user creating the Room.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created Room entity.</returns>
    public Task<Room?> CreateRoomAsync(string username, Guid uuid, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the reveal status for a room.
    /// </summary>
    /// <param name="roomCode">The unique code identifying the room.</param>
    /// <param name="revealStatus">The new status</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result indicates whether the reveal status was successfully set.</returns>
    public Task<bool> SetRevealStatusAsync(Guid roomCode, bool revealStatus, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the topic for a room.
    /// </summary>
    /// <param name="roomCode">The unique code identifying the room.</param>
    /// <param name="uuid">The UUID associated with the topic.</param>
    /// <param name="topic">The topic to be set for the room.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>True if the topic was successfully set for the room; otherwise, false.</returns>
    public Task<bool> SetTopicAsync(Guid roomCode, Guid uuid, string topic, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a user to a room asynchronously.
    /// </summary>
    /// <param name="roomCode">The unique code of the room.</param>
    /// <param name="uuid">The unique identifier of the user.</param>
    /// <param name="username">Username of the user</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result indicates whether the user was successfully added to the room.</returns>
    public Task<bool> AddUserAsync(Guid roomCode, Guid uuid, string username, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Delete a user from a specific room.
    /// </summary>
    /// <param name="roomCode">The unique code of the room.</param>
    /// <param name="uuid">The unique identifier of the user.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result indicates whether the user was successfully added to the room.</returns>
    public Task<bool> RemoveUserAsync(Guid roomCode, Guid uuid, CancellationToken cancellationToken = default);
}