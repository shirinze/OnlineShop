using MediatR;

namespace OnlineShop.Commands.UserEntity.Create;

public record  CreateUserEntityCommand(string FirstName,string LastName,string Phone):IRequest
{
}
