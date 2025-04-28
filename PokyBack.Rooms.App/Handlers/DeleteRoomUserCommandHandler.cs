using MediatR;
using PokyBack.Rooms.App.Commands;
using PokyBack.Rooms.Core.Abstractions;

namespace PokyBack.Rooms.App.Handlers;

public class DeleteRoomUserCommandHandler(IRoomRepository repository) : IRequestHandler<DeleteRoomUserCommand, bool>
{
    public async Task<bool> Handle(DeleteRoomUserCommand request, CancellationToken cancellationToken)
    {
        return await repository.RemoveUserAsync(request.RoomId, request.Uuid, cancellationToken);
    }
}