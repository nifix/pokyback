using FluentAssertions;
using Moq;
using PokyBack.Rooms.App.Commands;
using PokyBack.Rooms.App.Handlers;
using PokyBack.Rooms.Core.Abstractions;
using PokyBack.Shared.Core.Entities;

namespace PokyBack.Tests.Rooms;

public class CreateRoomCommandHandlerTests
{
    private readonly Mock<IRoomRepository> _mockRoomRepository;
    private readonly CreateRoomCommandHandler _handlerCommand;
    private readonly CancellationToken _cancellationToken;

    public CreateRoomCommandHandlerTests()
    {
        _mockRoomRepository = new Mock<IRoomRepository>();
        _handlerCommand = new CreateRoomCommandHandler(_mockRoomRepository.Object);

        _cancellationToken = CancellationToken.None;
    }

    /// <summary>
    /// Tests the Handle method to verify that it creates and returns
    /// a Room when the repository successfully creates a new Room.
    /// </summary>
    /// <returns>A task that represents the asynchronous test.
    /// The task result ensures the method being tested returns a valid Room instance
    /// that meets the specified conditions when created successfully in the repository.</returns>
    [Fact]
    public async Task Handle_WhenRepositoryCreatesRoom_ShouldCreateRoom()
    {
        // Arrange
        var creatorUuid = Guid.NewGuid();
        const string username = "Friteuse";
        
        var newRoom = new Room().InitializeNewRoom(username, creatorUuid);
        var command = new CreateRoomCommand(username, creatorUuid);

        _mockRoomRepository
            .Setup(r => r.CreateRoomAsync(It.IsAny<string>(), It.IsAny<Guid>(), _cancellationToken))
            .ReturnsAsync(() => newRoom);
        
        // Act
        var result = await _handlerCommand.Handle(command, _cancellationToken);
        
        // Assert
        result.Should().NotBeNull();
        result.Code
            .Should().BeAssignableTo<string>()
            .Should().NotBeNull();
        
        result.CreatedByUuid
            .Should().BeAssignableTo<string>()
            .Should().NotBeNull();
        
        result.Topic.Should().Be("No topic set");
        _mockRoomRepository.Verify(r => r.CreateRoomAsync(It.IsAny<string>(), It.IsAny<Guid>(), _cancellationToken), Times.Once);
    }

    /// <summary>
    /// Tests the Handle method to verify that it returns null
    /// when the repository fails to create a Room.
    /// </summary>
    /// <returns>A task that represents the asynchronous test.
    /// The task result is void but ensures the method being tested returns null
    /// when a Room is not created in the repository.</returns>
    [Fact]
    public async Task Handle_WhenRepositoryDoesNotCreateRoom_ShouldReturnNull()
    {
        // Arrange
        const string username = "Friteuse";
        var creatorUuid = Guid.NewGuid();
        var command = new CreateRoomCommand(username, creatorUuid);

        _mockRoomRepository
            .Setup(r => r.CreateRoomAsync(It.IsAny<string>(), It.IsAny<Guid>(), _cancellationToken))
            .ReturnsAsync(() => null);
        
        // Act
        var result = await _handlerCommand.Handle(command, _cancellationToken);
        
        // Assert
        result.Should().BeNull();
        _mockRoomRepository.Verify(r => r.CreateRoomAsync(It.IsAny<string>(), It.IsAny<Guid>(), _cancellationToken), Times.Once);
    }
}