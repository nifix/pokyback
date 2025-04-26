using MediatR;
using PokyBack.Shared.Core.Entities;

namespace PokyBack.Rooms.App.Commands;

public record CreateRoomCommand(string Username, Guid Uuid) : IRequest<Room?>;