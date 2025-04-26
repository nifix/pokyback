using MediatR;
using PokyBack.Shared.Core.Entities;

namespace PokyBack.Rooms.App.Queries;

public record GetRoomByIdQuery(int Id): IRequest<Room>;