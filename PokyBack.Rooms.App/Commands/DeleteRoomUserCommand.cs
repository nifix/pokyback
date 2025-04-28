using MediatR;

namespace PokyBack.Rooms.App.Commands;

public record DeleteRoomUserCommand(Guid RoomId, Guid Uuid) : IRequest<bool>;