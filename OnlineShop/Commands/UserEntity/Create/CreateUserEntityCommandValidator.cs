using FluentValidation;

namespace OnlineShop.Commands.UserEntity.Create;

public class CreateUserEntityCommandValidator:AbstractValidator<CreateUserEntityCommand>
{
    public CreateUserEntityCommandValidator()
    {
        RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("firstname is required").
            MaximumLength(50).WithMessage("firstname must be under 50 charectors");

        RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("lastname is required").
            MaximumLength(50).WithMessage("lastname must be under 50 charectors");

        
    }

}
