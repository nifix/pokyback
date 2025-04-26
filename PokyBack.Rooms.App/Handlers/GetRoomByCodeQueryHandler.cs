using MediatR;
using PokyBack.Rooms.App.Queries;
using PokyBack.Rooms.Core.Abstractions;
using PokyBack.Shared.Core.Entities;

namespace PokyBack.Rooms.App.Handlers;

public class GetRoomByCodeQueryHandler(IRoomRepository repository) : IRequestHandler<GetRoomByCodeQuery, Room?>
{
    public async Task<Room?> Handle(GetRoomByCodeQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetByCodeAsync(request.Code, cancellationToken);
    }
}