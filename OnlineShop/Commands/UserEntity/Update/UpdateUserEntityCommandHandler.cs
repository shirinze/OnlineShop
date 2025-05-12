using MediatR;
using OnlineShop.Exceptions;
using OnlineShop.Services;

namespace OnlineShop.Commands.UserEntity.Update;

public class UpdateUserEntityCommandHandler(IUserEntityService service) : IRequestHandler<UpdateUserEntityCommand>
{
    public async Task Handle(UpdateUserEntityCommand request, CancellationToken cancellationToken)
    {
        await service.UpdateAsync(request.Id, request.FirstName, request.LastName, request.Phone,cancellationToken);
    }
}
