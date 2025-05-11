using MediatR;

namespace OnlineShop.Commands.UserEntity.ToggleActivation;

public record ToggleActivationUserEntityCommand(int Id):IRequest
{
}
