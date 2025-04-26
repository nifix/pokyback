using MediatR;
using PokyBack.Rooms.App.Queries;
using PokyBack.Rooms.Core.Abstractions;
using PokyBack.Shared.Core.Entities;

namespace PokyBack.Rooms.App.Handlers;

public class GetAllRoomsQueryHandler(IRoomRepository repository) : IRequestHandler<GetAllRoomsQuery, List<Room>>
{
    public async Task<List<Room>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetAllAsync(cancellationToken);
    }
}
