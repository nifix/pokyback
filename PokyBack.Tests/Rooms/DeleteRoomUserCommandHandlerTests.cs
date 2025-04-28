using FluentAssertions;
using Moq;
using PokyBack.Rooms.App.Commands;
using PokyBack.Rooms.App.Handlers;
using PokyBack.Rooms.Core.Abstractions;
using PokyBack.Shared.Core.Entities;

namespace PokyBack.Tests.Rooms;

public class DeleteRoomUserCommandHandlerTests
{
    private readonly Mock<IRoomRepository> _mockRoomRepository;
    private readonly DeleteRoomUserCommandHandler _handlerCommand;
    private readonly CancellationToken _cancellationToken;

    public DeleteRoomUserCommandHandlerTests()
    {
        _mockRoomRepository = new Mock<IRoomRepository>();
        _handlerCommand = new DeleteRoomUserCommandHandler(_mockRoomRepository.Object);

        _cancellationToken = CancellationToken.None;
    }

    /// <summary>
    /// Tests the Handle method to verify that it returns true
    /// when the repository successfully deletes a specified user from a Room.
    /// </summary>
    /// <returns>A task that represents the asynchronous test.
    /// The task result ensures the method being tested returns true
    /// when the repository deletes the user as requested.</returns>
    [Fact]
    public async Task Handle_WhenRepositoryDeletesAnUser_ShouldReturnTrue()
    {
        // Arrange
        var userToDelete = Guid.NewGuid();
        var roomToDelete = Guid.NewGuid();
        
        var command = new DeleteRoomUserCommand(roomToDelete, userToDelete);

        _mockRoomRepository
            .Setup(r => r.RemoveUserAsync(roomToDelete, userToDelete, _cancellationToken))
            .ReturnsAsync(true);
        
        // Act
        var result = await _handlerCommand.Handle(command, _cancellationToken);
        
        // Assert
        result.Should().BeTrue();
    }

    /// <summary>
    /// Tests the Handle method to verify that it returns false
    /// when the repository fails to delete a specified user from a Room.
    /// </summary>
    /// <returns>A task that represents the asynchronous test.
    /// The task result ensures the method being tested returns false
    /// when the repository does not delete the user as requested.</returns>
    [Fact]
    public async Task Handle_WhenRepositoryDoesNotDeletesAnUser_ShouldReturnFalse()
    {
        // Arrange
        var userToDelete = Guid.NewGuid();
        var roomToDelete = Guid.NewGuid();
        
        var command = new DeleteRoomUserCommand(roomToDelete, userToDelete);

        _mockRoomRepository
            .Setup(r => r.RemoveUserAsync(roomToDelete, userToDelete, _cancellationToken))
            .ReturnsAsync(false);
        
        // Act
        var result = await _handlerCommand.Handle(command, _cancellationToken);
        
        // Assert
        result.Should().BeFalse();
    }
}