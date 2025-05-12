using MediatR;
using OnlineShop.Exceptions;
using OnlineShop.Services;

namespace OnlineShop.Commands.UserEntity.Update;

public class UpdateUserEntityCommandHandler(IUserEntityService service) : IRequestHandler<UpdateUserEntityCommand>
{
    public async Task Handle(UpdateUserEntityCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateUserEntityCommandValidator();
        var result = validator.Validate(request);
        if (!result.IsValid)
        {
            throw new BadRequestException(string.Join(',', result.Errors));
        }
        await service.UpdateAsync(request.Id, request.FirstName, request.LastName, request.Phone,cancellationToken);
    }
}
