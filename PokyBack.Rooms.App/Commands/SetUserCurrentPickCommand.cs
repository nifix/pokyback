using MediatR;

namespace PokyBack.Rooms.App.Commands;

public record SetUserCurrentPickCommand(Guid RoomId, Guid Uuid, int PickedCard) : IRequest<bool>;