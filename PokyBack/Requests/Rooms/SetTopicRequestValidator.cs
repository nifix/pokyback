using FluentValidation;

namespace PokyBack.Requests.Rooms;

public class SetTopicRequestValidator : AbstractValidator<SetTopicRequest>
{
    public SetTopicRequestValidator()
    {
        RuleFor(s => s.Uuid).NotEmpty().NotNull();
    }
}