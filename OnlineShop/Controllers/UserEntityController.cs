using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Commands.UserEntity.Create;
using OnlineShop.Commands.UserEntity.Delete;
using OnlineShop.Commands.UserEntity.ToggleActivation;
using OnlineShop.Commands.UserEntity.Update;
using OnlineShop.DTOs;
using OnlineShop.Features;
using OnlineShop.Models;
using OnlineShop.Queries.UserEntity.GetById;
using OnlineShop.Queries.UserEntity.GetList;
using OnlineShop.Services;
using OnlineShop.ViewModels;

namespace OnlineShop.Controllers;

[Route("UserEntities")]
[ApiController]
public class UserEntityController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateOrUpdateUserDto input,CancellationToken cancellationToken)
    {
        var command=new CreateUserEntityCommand(input.FirstName, input.LastName,input.Phone);
        await mediator.Send(command,cancellationToken);
        
        return Ok(BaseResult.Success());
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateUser([FromRoute]int id, [FromBody] CreateOrUpdateUserDto input,CancellationToken cancellationToken)
    {
        var command = new UpdateUserEntityCommand(id, input.FirstName, input.LastName, input.Phone);
        await mediator.Send(command, cancellationToken);
        return Ok(BaseResult.Success());
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUser([FromRoute] int id,CancellationToken cancellationToken)
    {
        var command = new DeleteUserEntityCommand(id);
        await mediator.Send(command, cancellationToken);
        return Ok(BaseResult.Success());
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserById([FromRoute] int id,CancellationToken cancellationToken)
    {
        var query = new GetUserEntityByIdQuery(id);
        var value = await mediator.Send(query, cancellationToken);
        return Ok(BaseResult.Success(value));
    }
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] string? q,
        [FromQuery] OrderType? orderType,
        [FromQuery] int? pageSize,
        [FromQuery] int? pageNumber,
        CancellationToken cancellationToken)
    {
        var queries = new GetUserEntityListQuery(q, orderType, pageSize, pageNumber);
        var values = await mediator.Send(queries, cancellationToken);
        return Ok(BaseResult.Success(values));
    }
    [HttpPut("{id:int}/Active")]
    public async Task<IActionResult> ActiveUser([FromRoute] int id,CancellationToken cancellationToken)
    {
        var command = new ToggleActivationUserEntityCommand(id);
        await mediator.Send(command,cancellationToken);
        return Ok(BaseResult.Success());
    }
    [HttpPut("{id:int}/DeActive")]
    public async Task<IActionResult> DeActiveUser([FromRoute] int id, CancellationToken cancellationToken)
    {
        var command = new ToggleActivationUserEntityCommand(id);
        await mediator.Send(command, cancellationToken);
        return Ok(BaseResult.Success());
    }


}
