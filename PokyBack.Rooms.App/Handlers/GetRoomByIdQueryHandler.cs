using MediatR;
using PokyBack.Rooms.App.Queries;
using PokyBack.Rooms.Core.Abstractions;
using PokyBack.Shared.Core.Entities;

namespace PokyBack.Rooms.App.Handlers;

public class GetRoomByIdQueryHandler(IRoomRepository repository): IRequestHandler<GetRoomByIdQuery, Room?>
{
    public async Task<Room?> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetByIdAsync(request.Id, cancellationToken);
    }
}