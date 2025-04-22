using OnlineShop.Models;

namespace OnlineShop.Services;

public interface IUserEntityService
{
    public Task CreateAsync(string firstName,string lastName,string phone,CancellationToken cancellationToken);
    public Task UpdateAsync(int id,string firstName,string lastName,string phone,CancellationToken cancellationToken);
    public Task DeleteAsync(int id,CancellationToken cancellationToken);
    public Task<UserEntity> GetByIdAsync(int id, CancellationToken cancellationToken);
    public Task<List<UserEntity>> GetListAsync(CancellationToken cancellationToken);
    public Task ToggleActivationAsync(int id, CancellationToken cancellationToken);
}
