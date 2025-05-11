using MediatR;
using OnlineShop.Features;
using OnlineShop.Services;
using OnlineShop.ViewModels;

namespace OnlineShop.Queries.UserEntity.GetList;

public class GetUserEntityListQueryHandler(IUserEntityService service) : IRequestHandler<GetUserEntityListQuery, PaginationResult<UserViewModel>>
{
    public async Task<PaginationResult<UserViewModel>> Handle(GetUserEntityListQuery request, CancellationToken cancellationToken)
    {
        return await service.GetListAsync(request.Q,request.OrderType,request.PageSize,request.PageNumber,cancellationToken);
    }
}
