using MediatR;
using PokyBack.Rooms.App.Commands;
using PokyBack.Rooms.Core.Abstractions;

namespace PokyBack.Rooms.App.Handlers;

public class SetTopicCommandHandler(IRoomRepository repository) : IRequestHandler<SetTopicCommand, bool>
{
    public async Task<bool> Handle(SetTopicCommand request, CancellationToken cancellationToken)
    {
        var result = await repository.SetTopicAsync(request.RoomCode, request.Uuid, request.Topic, cancellationToken);
        return result;
    }
}