using MediatR;
using PokyBack.Rooms.App.Commands;
using PokyBack.Rooms.Core.Abstractions;
using PokyBack.Shared.Core.Abstractions;

namespace PokyBack.Rooms.App.Handlers;

public class SetUserCurrentPickCommandHandler(IRoomUserRepository repository, ILogRepository logRepository) : IRequestHandler<SetUserCurrentPickCommand, bool>
{
    public async Task<bool> Handle(SetUserCurrentPickCommand request, CancellationToken cancellationToken)
    {
        var result = await repository.SetUserPickAsync(request.RoomId, request.Uuid, request.PickedCard, cancellationToken);
        if (result)
            await logRepository.AddLog("user_picked_card", request.RoomId.ToString(), request.PickedCard, userUuid: request.Uuid.ToString());
        
        return result;
    }
}