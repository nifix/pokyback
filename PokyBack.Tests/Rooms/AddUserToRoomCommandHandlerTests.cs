using FluentAssertions;
using Moq;
using PokyBack.Rooms.App.Commands;
using PokyBack.Rooms.App.Handlers;
using PokyBack.Rooms.Core.Abstractions;
using PokyBack.Shared.Core.Entities;

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
    
    [Fact]
    public async Task Handle_WhenRepositoryAddsUser_ShouldCreatesUser()
    {
        _mockRoomRepository
            .Setup(r => r.AddUserAsync(_targetGuid, It.IsAny<Guid>(), It.IsAny<string>(), _cancellationToken))
            .ReturnsAsync(true);
        
        var successfulCommand = new AddUserToRoomCommand(_targetGuid, _targetUuid, _targetUserName);
        var unsuccessfulCommand = new AddUserToRoomCommand(Guid.NewGuid(),_targetUuid, string.Empty);
        
        // Act
        var successfulResult = await _handlerTests.Handle(successfulCommand, _cancellationToken);
        var roomDoesntExist = await _handlerTests.Handle(unsuccessfulCommand, _cancellationToken);
        
        // Assert
        successfulResult.Should().BeTrue();
        roomDoesntExist.Should().BeFalse();
    }
}