using FluentValidation;

namespace PokyBack.Requests.Rooms;

public class SetRevealStatusRequestValidator : AbstractValidator<SetRevealStatusRequest>
{
    public SetRevealStatusRequestValidator()
    {
        RuleFor(s => s.RevealStatus).NotNull();
    }
}