using MediatR;
using PokyBack.Rooms.App.Commands;
using PokyBack.Rooms.Core.Abstractions;
using PokyBack.Shared.Core.Entities;

namespace PokyBack.Rooms.App.Handlers;

public class CreateRoomCommandHandler(IRoomRepository repository) : IRequestHandler<CreateRoomCommand, Room?>
{
    public async Task<Room?> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        return await repository.CreateRoomAsync(request.Username, request.Uuid, cancellationToken);
    }
}