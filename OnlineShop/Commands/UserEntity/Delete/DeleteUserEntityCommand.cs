using MediatR;

namespace OnlineShop.Commands.UserEntity.Delete;

public record DeleteUserEntityCommand(int Id):IRequest
{
}
