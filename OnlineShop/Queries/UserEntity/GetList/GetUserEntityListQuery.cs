using MediatR;
using OnlineShop.Features;
using OnlineShop.ViewModels;

namespace OnlineShop.Queries.UserEntity.GetList;

public record GetUserEntityListQuery(string? Q,
    OrderType? OrderType,
    int? PageSize,
    int? PageNumber
    )
    :IRequest<PaginationResult<UserViewModel>>
{
}
