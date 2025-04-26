using FluentAssertions;
using Moq;
using PokyBack.Rooms.App.Handlers;
using PokyBack.Rooms.App.Queries;
using PokyBack.Rooms.Core.Abstractions;
using PokyBack.Shared.Core.Entities;


namespace PokyBack.Tests.Rooms;

public class GetRoomByIdQueryHandlerTests
{
    private readonly Mock<IRoomRepository> _mockRoomRepository;
    private readonly GetRoomByIdQueryHandler _queryHandler;
    private readonly GetRoomByIdQuery _query;
    private readonly CancellationToken _cancellationToken;

    public GetRoomByIdQueryHandlerTests()
    {
        _mockRoomRepository = new Mock<IRoomRepository>();
        _queryHandler = new GetRoomByIdQueryHandler(_mockRoomRepository.Object);
        _query = new GetRoomByIdQuery(1);
        _cancellationToken = CancellationToken.None;
    }

    [Fact]
    public async Task Handle_WhenRepositoryReturnsRoom_ShouldReturnsRoom()
    {
        // Arrange
        var expectedRoom = new Room
        {
            Id = 1, Code = "123456789", CreatedBy = "Test", CreatedAt = DateTime.Now, IsRevealed = true
        };
        
        _mockRoomRepository
            .Setup(r => r.GetByIdAsync(_query.Id, _cancellationToken))
            .ReturnsAsync(expectedRoom);
        
        // Act
        var result = await _queryHandler.Handle(_query, _cancellationToken);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedRoom);
        
        _mockRoomRepository.Verify(r => r.GetByIdAsync(_query.Id, _cancellationToken), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenRepositoryReturnsNull_ShouldReturnsNull()
    {
        // Arrange
        _mockRoomRepository
            .Setup(r => r.GetByIdAsync(_query.Id, _cancellationToken))
            .ReturnsAsync((Room?)null);
        
        // Act
        var result = await _queryHandler.Handle(_query, _cancellationToken);
        
        // Assert
        result.Should().BeNull();
        
        _mockRoomRepository.Verify(r => r.GetByIdAsync(_query.Id, _cancellationToken), Times.Once);
    }
}