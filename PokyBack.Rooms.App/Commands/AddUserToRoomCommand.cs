using MediatR;

namespace PokyBack.Rooms.App.Commands;

public record AddUserToRoomCommand(Guid RoomId, Guid Uuid, string Username) : IRequest<bool>;