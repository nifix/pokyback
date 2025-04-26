using MediatR;
using PokyBack.Shared.Core.Entities;

namespace PokyBack.Rooms.App.Queries;

public record GetRoomByCodeQuery(Guid Code): IRequest<Room?>;