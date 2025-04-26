using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokyBack.Responses;
using PokyBack.Requests.Rooms;
using PokyBack.Rooms.App.Commands;
using PokyBack.Rooms.App.Queries;

namespace PokyBack.Controllers;

[Route("api/[controller]")]
public class RoomsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IResult> Get()
    {
        var rooms = await mediator.Send(new GetAllRoomsQuery());
        return Results.Ok(rooms);
    }
    
    /// <summary>
    /// [id].get.ts
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<IResult> GetRoomById(int id)
    {
        var room = await mediator.Send(new GetRoomByIdQuery(id));
        return Results.Ok(room);
    }

    /// <summary>
    /// query.get
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    [HttpGet("{code:guid}")]
    public async Task<IResult> GetRoomByCode(Guid code)
    {
        var room = await mediator.Send(new GetRoomByCodeQuery(code));
        var result = room is null 
            ? Results.NotFound() 
            : Results.Ok(new RoomResponse().LoadFromEntity(room));
        
        return result;
    }
    
    /// <summary>
    /// index.post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IResult> CreateNewRoom([FromBody] CreateRoomRequest request)
    {
        // Validating the payload
        var requestValidator = new CreateRoomRequestValidator();
        var validationResult = await requestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            return Results.BadRequest(validationResult.Errors);
        
        // Creating the room
        var room = await mediator.Send(new CreateRoomCommand(request.Username!, request.Uuid ?? Guid.Empty));

        if (room is null) 
            return Results.BadRequest("Something went wrong while creating the room.");
        
        var result = new RoomResponse().LoadFromEntity(room);
        return Results.Created("Created", result);
    }

    /// <summary>
    /// Reveals or hides votes for a room.
    /// [code]/reveal.put
    /// </summary>
    /// <param name="roomCode">The code of the room to reveal votes for.</param>
    /// <param name="revealStatusRequest">True to reveal votes, false to hide them.</param>
    /// <returns></returns>
    [HttpPut("{roomCode:guid}/reveal")]
    public async Task<IResult> RevealVotes([FromBody] SetRevealStatusRequest revealStatusRequest, Guid roomCode)
    {
        var requestValidator = new SetRevealStatusRequestValidator();
        var validationResult = await requestValidator.ValidateAsync(revealStatusRequest);
        if (!validationResult.IsValid)
            return Results.BadRequest(validationResult.Errors);
        
        var result= await mediator.Send(new SetRevealStatusCommand(roomCode, revealStatusRequest.RevealStatus!.Value));
        return result ? Results.Ok(result) : Results.BadRequest("Failed to reveal the status");
    }

    /// <summary>
    /// Sets the topic for a room.
    /// </summary>
    /// <param name="topicRequest">The request containing the UUID and topic.</param>
    /// <param name="roomCode">The code of the room to set the topic for.</param>
    /// <returns>True if the topic was set successfully, otherwise false.</returns>
    [HttpPut("{roomCode:guid}/topic")]
    public async Task<IResult> SetTopic([FromBody] SetTopicRequest topicRequest, Guid roomCode)
    {
        var requestValidator = new SetTopicRequestValidator();
        var validationResult = await requestValidator.ValidateAsync(topicRequest);
        if (!validationResult.IsValid)
            return Results.BadRequest(validationResult.Errors);
        
        var result= await mediator.Send(new SetTopicCommand(roomCode, topicRequest.Uuid!, topicRequest.Topic ?? string.Empty));;
        return result ? Results.Ok(result) : Results.BadRequest("Failed to update topic");
    }

    /// <summary>
    /// Adds a user to a room.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="roomCode"></param>
    /// <returns></returns>
    [HttpPost("{roomCode:guid}/users")]
    public async Task<IResult> AddUserToRoom([FromBody] AddUserToRoomRequest request, Guid roomCode)
    {
        // Guid roomCode, Guid uuid, string username
        var requestValidator = new AddUserToRoomRequestValidator();
        var validationResult = await requestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            return Results.BadRequest(validationResult.Errors);
        
        var result = await mediator.Send(new AddUserToRoomCommand(roomCode, request.Uuid, request.Username!));
        return result ? Results.Created("Added user", result) : Results.BadRequest("Failed to add user, unknown room code");
    }

    /// <summary>
    /// Set a user current picked card
    /// </summary>
    /// <param name="request"></param>
    /// <param name="roomCode"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpPut("{roomCode:guid}/users/{userId:guid}")]
    public async Task<IResult> SetUserCurrentPick([FromBody] SetUserCurrentPickRequest request, Guid roomCode,
        Guid userId)
    {
        var result = await mediator.Send(new SetUserCurrentPickCommand(roomCode, userId, request.CurrentPickedCard));
        return result ? Results.Ok(result) : Results.BadRequest("Failed to set current pick");
    }
}