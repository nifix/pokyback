using MediatR;
using PokyBack.Rooms.App.Queries;
using PokyBack.Rooms.Core.Abstractions;
using PokyBack.Shared.Core.Entities;

namespace PokyBack.Rooms.App.Handlers;

public class GetRoomUsersQueryHandler(IRoomUserRepository repository) : IRequestHandler<GetRoomUsersQuery, List<RoomUser>>
{
    public async Task<List<RoomUser>> Handle(GetRoomUsersQuery request, CancellationToken cancellationToken)
    {
        var results = await repository.GetRoomUsersAsync(request.RoomId, cancellationToken);
        return results;
    }
}