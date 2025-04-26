using FluentValidation;

namespace PokyBack.Requests.Rooms;

public class AddUserToRoomRequestValidator : AbstractValidator<AddUserToRoomRequest>
{
    public AddUserToRoomRequestValidator()
    {
        RuleFor(s => s.Username).NotEmpty().NotNull();
        RuleFor(s => s.Uuid).NotEmpty().NotNull();
    }    
}