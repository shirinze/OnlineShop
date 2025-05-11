using MediatR;

namespace OnlineShop.Commands.UserEntity.Update;

public record UpdateUserEntityCommand(int Id,string FirstName,string LastName,string Phone):IRequest
{
}
