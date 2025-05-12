using MediatR;
using OnlineShop.Exceptions;
using OnlineShop.Services;

namespace OnlineShop.Commands.UserEntity.Create;

public class CreateUserEntityCommandHandler(IUserEntityService service) : IRequestHandler<CreateUserEntityCommand>
{
    public async Task Handle(CreateUserEntityCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateUserEntityCommandValidator();
        var result = validator.Validate(request);
        if (!result.IsValid)
        {
            throw new BadRequestException(string.Join(',',result.Errors));
        }
        await service.CreateAsync(request.FirstName, request.LastName, request.Phone, cancellationToken);
    }
}
