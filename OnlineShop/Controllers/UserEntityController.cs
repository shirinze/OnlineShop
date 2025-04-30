using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.DTOs;
using OnlineShop.Models;
using OnlineShop.Services;
using OnlineShop.ViewModels;

namespace OnlineShop.Controllers;

[Route("UserEntities")]
[ApiController]
public class UserEntityController(IUserEntityService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateOrUpdateUserDto input,CancellationToken cancellationToken)
    {
        await service.CreateAsync(input.FirstName, input.LastName, input.Phone, cancellationToken);
        return Ok("Success");
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateUser([FromRoute]int id, [FromBody] CreateOrUpdateUserDto input,CancellationToken cancellationToken)
    {
        await service.UpdateAsync(id, input.FirstName, input.LastName, input.Phone, cancellationToken);
        return Ok("Success");
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUser([FromRoute] int id,CancellationToken cancellationToken)
    {
        await service.DeleteAsync(id, cancellationToken);
        return Ok("Success");
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserById([FromRoute] int id,CancellationToken cancellationToken)
    {
        var value = await service.GetByIdAsync(id, cancellationToken);
        return Ok(value);
    }
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] string? q,CancellationToken cancellationToken)
    {
        var values = await service.GetListAsync(q,cancellationToken);
        return Ok(values);
    }
    [HttpPut("{id:int}/Active")]
    public async Task<IActionResult> ActiveUser([FromRoute] int id,CancellationToken cancellationToken)
    {
        await service.ToggleActivationAsync(id, cancellationToken);
        return Ok("Success");
    }
    [HttpPut("{id:int}/DeActive")]
    public async Task<IActionResult> DeActiveUser([FromRoute] int id, CancellationToken cancellationToken)
    {
        await service.ToggleActivationAsync(id, cancellationToken);
        return Ok("Success");
    }


}
