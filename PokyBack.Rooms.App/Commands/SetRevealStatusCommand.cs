using MediatR;

namespace PokyBack.Rooms.App.Commands;

public record SetRevealStatusCommand(Guid roomCode, bool revealStatus) : IRequest<bool>;