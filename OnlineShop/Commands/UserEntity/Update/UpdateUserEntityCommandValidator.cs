using FluentValidation;

namespace OnlineShop.Commands.UserEntity.Update;

public class UpdateUserEntityCommandValidator:AbstractValidator<UpdateUserEntityCommand>
{
    public UpdateUserEntityCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("firstname is required").
            MaximumLength(50).WithMessage("firstname must be under 50 charectors");

        RuleFor(x => x.LastName).NotEmpty().WithMessage("lastname is required").
            MaximumLength(50).WithMessage("lastname must be under 50 charectors");
    }
}
