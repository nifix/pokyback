using MediatR;
using PokyBack.Shared.Core.Entities;

namespace PokyBack.Rooms.App.Queries;

public record GetRoomUsersQuery(Guid RoomId) : IRequest<List<RoomUser>>;