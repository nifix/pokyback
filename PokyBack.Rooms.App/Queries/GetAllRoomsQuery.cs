using MediatR;
using PokyBack.Shared.Core.Entities;

namespace PokyBack.Rooms.App.Queries;

public record GetAllRoomsQuery : IRequest<List<Room>>;
