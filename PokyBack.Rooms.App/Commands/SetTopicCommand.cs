using MediatR;

namespace PokyBack.Rooms.App.Commands;

public record SetTopicCommand(Guid RoomCode, Guid Uuid, string Topic) : IRequest<bool>;