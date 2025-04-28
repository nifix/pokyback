using MediatR;

namespace PokyBack.Rooms.App.Commands;

public record SetRevealStatusCommand(Guid RoomCode, bool RevealStatus) : IRequest<bool>;