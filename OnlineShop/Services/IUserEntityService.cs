using OnlineShop.Features;
using OnlineShop.Models;

namespace OnlineShop.Services;

public interface IUserEntityService
{
    public Task CreateAsync(string firstName,string lastName,string phone,CancellationToken cancellationToken);
    public Task UpdateAsync(int id,string firstName,string lastName,string phone,CancellationToken cancellationToken);
    public Task DeleteAsync(int id,CancellationToken cancellationToken);
    public Task<UserEntity> GetByIdAsync(int id, CancellationToken cancellationToken);
    public Task<PaginationResult<UserEntity>> GetListAsync(string? q, OrderType? orderType,int? pageSize,int? pageNumber, CancellationToken cancellationToken);
    public Task ToggleActivationAsync(int id, CancellationToken cancellationToken);
}
