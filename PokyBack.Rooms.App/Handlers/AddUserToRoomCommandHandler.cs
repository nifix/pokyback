using MediatR;
using PokyBack.Rooms.App.Commands;
using PokyBack.Rooms.Core.Abstractions;

namespace PokyBack.Rooms.App.Handlers;

public class AddUserToRoomCommandHandler(IRoomRepository repository) : IRequestHandler<AddUserToRoomCommand, bool>
{
    public async Task<bool> Handle(AddUserToRoomCommand request, CancellationToken cancellationToken)
    {
        var result = await repository.AddUserAsync(request.RoomId, request.Uuid, request.Username, cancellationToken);
        return result;
    }
}