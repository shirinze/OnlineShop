using MediatR;
using OnlineShop.Exceptions;
using OnlineShop.Services;

namespace OnlineShop.Commands.UserEntity.Create;

public class CreateUserEntityCommandHandler(IUserEntityService service) : IRequestHandler<CreateUserEntityCommand>
{
    public async Task Handle(CreateUserEntityCommand request, CancellationToken cancellationToken)
    {
        await service.CreateAsync(request.FirstName, request.LastName, request.Phone, cancellationToken);
    }
}
