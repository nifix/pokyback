using MediatR;
using PokyBack.Rooms.App.Commands;
using PokyBack.Rooms.Core.Abstractions;

namespace PokyBack.Rooms.App.Handlers;

public class SetRevealStatusCommandHandler(IRoomRepository roomRepository) : IRequestHandler<SetRevealStatusCommand, bool>
{
    public async Task<bool> Handle(SetRevealStatusCommand request, CancellationToken cancellationToken)
    {
        return await roomRepository.SetRevealStatusAsync(request.roomCode, request.revealStatus, cancellationToken);
    }
}