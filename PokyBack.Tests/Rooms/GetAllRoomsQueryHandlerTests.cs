using FluentAssertions;
using Moq;
using PokyBack.Rooms.App.Handlers;
using PokyBack.Rooms.App.Queries;
using PokyBack.Rooms.Core.Abstractions;
using PokyBack.Shared.Core.Entities;

namespace PokyBack.Tests.Rooms;

public class GetAllRoomsQueryHandlerTests
{
    private readonly Mock<IRoomRepository> _mockRoomRepository;
    private readonly GetAllRoomsQueryHandler _handler;
    private readonly GetAllRoomsQuery _query;
    private readonly CancellationToken _cancellationToken;

    public GetAllRoomsQueryHandlerTests()
    {
        _mockRoomRepository = new Mock<IRoomRepository>();
        _handler = new GetAllRoomsQueryHandler(_mockRoomRepository.Object);
        _query = new GetAllRoomsQuery();
        _cancellationToken = CancellationToken.None;
    }

    /// <summary>
    /// Ensures that when the repository returns a list of rooms, the method returns the same list of rooms.
    /// </summary>
    /// <returns>
    /// A task representing the asynchronous operation. The result contains a list of Room entities returned by the repository.
    /// </returns>
    [Fact]
    public async Task Handle_WhenRepositoryReturnsRooms_ShouldReturnsRooms()
    {
        // Arrange
        var expectedRooms = new List<Room>
        {
            new Room
            {
                Id = 1, Code = "123456789", CreatedBy = "Test", CreatedAt = DateTime.Now, IsRevealed = true,
                Topic = "TU 1"
            },
            new Room
            {
                Id = 1, Code = "123456789", CreatedBy = "Test 2", CreatedAt = DateTime.Now, IsRevealed = true,
                Topic = "TU 2"
            }
        };
        
        _mockRoomRepository
            .Setup(r => r.GetAllAsync(_cancellationToken))
            .ReturnsAsync(expectedRooms);
        
        // Act
        var result = await _handler.Handle(_query, _cancellationToken);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(expectedRooms.Count);
        result.Should().AllBeOfType<Room>();
        result.Should().BeEquivalentTo(expectedRooms);
        result.TrueForAll(s => s.Code == "123456789").Should().BeTrue();

        _mockRoomRepository.Verify(r => r.GetAllAsync(_cancellationToken), Times.Once);
    }

    /// <summary>
    /// Ensures that when the repository returns an empty list, the method returns an empty list.
    /// </summary>
    /// <returns>
    /// A task representing the asynchronous operation. The result contains an empty list of Room when the repository does not return any rooms.
    /// </returns>
    [Fact]
    public async Task Handle_WhenRepositoryReturnsEmptyList_ShouldReturnsEmptyList()
    {
        // Arrange
        var expectedRooms = new List<Room>();
        
        _mockRoomRepository
            .Setup(r => r.GetAllAsync(_cancellationToken))
            .ReturnsAsync(expectedRooms);
        
        // Act
        var result = await _handler.Handle(_query, _cancellationToken);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
        
        _mockRoomRepository.Verify(repo => repo.GetAllAsync(_cancellationToken), Times.Once);
    }
}