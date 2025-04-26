namespace PokyBack.Shared.Core.Abstractions;

public interface ILogRepository
{
    /// <summary>
    /// Adds a log entry to the database.
    /// </summary>
    /// <param name="code">The event code for the log entry.</param>
    /// <param name="roomCode">The room code associated with the log entry.</param>
    /// <param name="value">The value associated with the log entry (can be null).</param>
    /// <param name="userUuid">The user UUID associated with the log entry (can be null).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    Task AddLog(string code, string roomCode, object? value = null, string? userUuid = null, CancellationToken cancellationToken = default);
}