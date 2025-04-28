using FluentAssertions;
using Moq;
using PokyBack.Rooms.App.Commands;
using PokyBack.Rooms.App.Handlers;
using PokyBack.Rooms.Core.Abstractions;

namespace PokyBack.Tests.Rooms;

public class SetRevealStatusCommandHandlerTests
{
    private readonly Mock<IRoomRepository> _mockRoomRepository;
    private readonly SetRevealStatusCommandHandler _handlerCommand;
    private readonly CancellationToken _cancellationToken;
    private Guid _roomCode = Guid.NewGuid();

    public SetRevealStatusCommandHandlerTests()
    {
        _mockRoomRepository = new Mock<IRoomRepository>();
        _handlerCommand = new SetRevealStatusCommandHandler(_mockRoomRepository.Object);

        _cancellationToken = CancellationToken.None;
    }

    /// <summary>
    /// Tests the Handle method to verify that it returns true
    /// when the repository successfully sets the reveal status for a room.
    /// </summary>
    /// <returns>A task that represents the asynchronous test.
    /// The task result is void but ensures the method being tested returns true
    /// when the reveal status is successfully updated in the repository.</returns>
    [Fact]
    public async Task Handle_WhenRepositorySetRevealStatus_ShouldReturnTrue()
    {
        // Arrange
        var command = new SetRevealStatusCommand(_roomCode, true);

        _mockRoomRepository
            .Setup(r => r.SetRevealStatusAsync(_roomCode, true, _cancellationToken))
            .ReturnsAsync(() => true);
        
        // Act
        var result = await _handlerCommand.Handle(command, _cancellationToken);
        
        // Assert
        result.Should().BeTrue();
        _mockRoomRepository.Verify(r => r.SetRevealStatusAsync(command.RoomCode, command.RevealStatus, _cancellationToken), Times.Once);
    }
    
    [Fact]
    public async Task Handle_WhenRepositoryDoesNotSetRevealStatus_ShouldReturnFalse()
    {
        // Arrange
        var command = new SetRevealStatusCommand(_roomCode, true);

        _mockRoomRepository
            .Setup(r => r.SetRevealStatusAsync(_roomCode, true, _cancellationToken))
            .ReturnsAsync(() => false);
        
        // Act
        var result = await _handlerCommand.Handle(command, _cancellationToken);
        
        // Assert
        result.Should().BeFalse();
        _mockRoomRepository.Verify(r => r.SetRevealStatusAsync(command.RoomCode, command.RevealStatus, _cancellationToken), Times.Once);
    }
}