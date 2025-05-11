using MediatR;
using OnlineShop.Services;

namespace OnlineShop.Commands.UserEntity.Delete;

public class DeleteUserEntityCommandHandler(IUserEntityService service) : IRequestHandler<DeleteUserEntityCommand>
{
    public async Task Handle(DeleteUserEntityCommand request, CancellationToken cancellationToken)
    {
        await service.DeleteAsync(request.Id, cancellationToken);
    }
}
