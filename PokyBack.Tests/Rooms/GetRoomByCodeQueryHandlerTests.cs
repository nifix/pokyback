using FluentAssertions;
using Moq;
using PokyBack.Rooms.App.Handlers;
using PokyBack.Rooms.App.Queries;
using PokyBack.Rooms.Core.Abstractions;
using PokyBack.Shared.Core.Entities;

namespace PokyBack.Tests.Rooms;

public class GetRoomByCodeQueryHandlerTests
{
    private readonly Mock<IRoomRepository> _mockRoomRepository;
    private readonly GetRoomByCodeQueryHandler _queryHandler;
    private readonly GetRoomByCodeQuery _query;
    private readonly CancellationToken _cancellationToken;
    private readonly Guid _targetGuid;
    
    public GetRoomByCodeQueryHandlerTests()
    {
        _mockRoomRepository = new Mock<IRoomRepository>();
        _queryHandler = new GetRoomByCodeQueryHandler(_mockRoomRepository.Object);
        _targetGuid = new Guid("cdffa201-4d45-4f46-bc5e-18b416e6c2b6");
        _query = new GetRoomByCodeQuery(_targetGuid);
        _cancellationToken = CancellationToken.None;
    }
    
    private Room GenerateValidRoom()
    {
        return new Room
        {
            Id = 1, 
            Code = _targetGuid.ToString(), 
            CreatedBy = "Test", 
            CreatedAt = DateTime.Now,
            IsRevealed = true
        };
    }

    /// <summary>
    /// Tests the functionality of the GetRoomByCodeHandler when the repository returns a Room object.
    /// Ensures the returned Room matches the expected values.
    /// </summary>
    /// <returns>A Task representing the asynchronous test operation.</returns>
    [Fact]
    public async Task Handle_WhenRepositoryReturnsRoom_ShouldReturnsRoom()
    {
        // Arrange
        var expectedResult = GenerateValidRoom();
        
        _mockRoomRepository
            .Setup(r => r.GetByCodeAsync(_query.Code, _cancellationToken))
            .ReturnsAsync(expectedResult);
        
        // Act
        var result = await _queryHandler.Handle(_query, _cancellationToken);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().Be(expectedResult);
        result.Code.Should().Be(_targetGuid.ToString());
        result.Id.Should().Be(1);
    }
    
    [Fact]
    public async Task Handle_WhenRepositoryReturnsNull_ShouldReturnsNull()
    {
        // Arrange
        _mockRoomRepository
            .Setup(r => r.GetByCodeAsync(_query.Code, _cancellationToken))
            .ReturnsAsync((Room?)null);
        
        // Act
        var result = await _queryHandler.Handle(_query, _cancellationToken);
        
        // Assert
        result.Should().BeNull();
    }
}