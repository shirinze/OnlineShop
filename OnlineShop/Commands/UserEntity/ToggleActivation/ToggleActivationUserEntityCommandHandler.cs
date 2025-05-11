using MediatR;
using OnlineShop.Services;

namespace OnlineShop.Commands.UserEntity.ToggleActivation;

public class ToggleActivationUserEntityCommandHandler(IUserEntityService service) : IRequestHandler<ToggleActivationUserEntityCommand>
{
    public async Task Handle(ToggleActivationUserEntityCommand request, CancellationToken cancellationToken)
    {
        await service.ToggleActivationAsync(request.Id, cancellationToken);
    }
}
