using MediatR;
using OnlineShop.Services;
using OnlineShop.ViewModels;

namespace OnlineShop.Queries.UserEntity.GetById;

public class GetUserEntityByIdQueryHandler(IUserEntityService service) : IRequestHandler<GetUserEntityByIdQuery, UserViewModel>
{
    public async Task<UserViewModel> Handle(GetUserEntityByIdQuery request, CancellationToken cancellationToken)
    {
        return await service.GetByIdAsync(request.Id,cancellationToken);
    }
}
