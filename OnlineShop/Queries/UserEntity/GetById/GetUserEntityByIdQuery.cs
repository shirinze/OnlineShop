using MediatR;
using OnlineShop.ViewModels;

namespace OnlineShop.Queries.UserEntity.GetById;

public record GetUserEntityByIdQuery(int Id):IRequest<UserViewModel>
{
}
