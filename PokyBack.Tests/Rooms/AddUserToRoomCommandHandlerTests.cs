using FluentAssertions;
using Moq;
using PokyBack.Rooms.App.Commands;
using PokyBack.Rooms.App.Handlers;
using PokyBack.Rooms.Core.Abstractions;

namespace PokyBack.Tests.Rooms;

public class AddUserToRoomCommandHandlerTests
{
    private readonly Mock<IRoomRepository> _mockRoomRepository;
    private readonly AddUserToRoomCommandHandler _handlerTests;
    private readonly CancellationToken _cancellationToken;
    
    private readonly Guid _targetGuid = new Guid("cdffa201-4d45-4f46-bc5e-18b416e6c2b6");
    private readonly Guid _targetUuid = new Guid("cdffa201-4d45-4f46-bc5e-18b416e6c2b7");
    private readonly string _targetUserName = "Chaussette";

    public AddUserToRoomCommandHandlerTests()
    {
        _mockRoomRepository = new Mock<IRoomRepository>();
        _handlerTests = new AddUserToRoomCommandHandler(_mockRoomRepository.Object);
        
        _cancellationToken = CancellationToken.None;
    }

    /// <summary>
    /// Handles the AddUserToRoomCommand and adds a user to a room using the IRoomRepository.
    /// </summary>
    /// <param name="request">The AddUserToRoomCommand containing the room ID, user UUID, and username.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>True if the user was successfully added to the room; otherwise, false.</returns>
    [Fact]
    public async Task Handle_WhenRepositoryAddsUserToKnownRoom_ShouldCreatesUser()
    {
        // Arrange
        _mockRoomRepository
            .Setup(r => r.AddUserAsync(_targetGuid, It.IsAny<Guid>(), It.IsAny<string>(), _cancellationToken))
            .ReturnsAsync(true);
        
        var successfulCommand = new AddUserToRoomCommand(_targetGuid, _targetUuid, _targetUserName);
        
        // Act
        var successfulResult = await _handlerTests.Handle(successfulCommand, _cancellationToken);
        
        // Assert
        successfulResult.Should().BeTrue();
        _mockRoomRepository.Verify(r => r.AddUserAsync(_targetGuid, _targetUuid, _targetUserName, _cancellationToken), Times.Once);
    }

    /// <summary>
    /// Verifies that when a request to add a user to an unknown room is handled, the operation is not performed.
    /// </summary>
    /// <returns>False, indicating that the user was not added to the room.</returns>
    [Fact]
    public async Task Handle_WhenRepositoryAddsUserToUnknownRoom_ShouldNotCreatesUser()
    {
        // Arrange
        _mockRoomRepository
            .Setup(r => r.AddUserAsync(_targetGuid, It.IsAny<Guid>(), It.IsAny<string>(), _cancellationToken))
            .ReturnsAsync(false);
        
        var unsuccessfulCommand = new AddUserToRoomCommand(Guid.NewGuid(), _targetUuid, _targetUserName);
        
        // Act
        var roomDoesntExist = await _handlerTests.Handle(unsuccessfulCommand, _cancellationToken);
        
        // Assert
        roomDoesntExist.Should().BeFalse();
        _mockRoomRepository.Verify(r => r.AddUserAsync(unsuccessfulCommand.RoomId, unsuccessfulCommand.Uuid, unsuccessfulCommand.Username, _cancellationToken), Times.Once);
    }
}