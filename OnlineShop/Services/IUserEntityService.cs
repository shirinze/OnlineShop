using OnlineShop.Models;
using OnlineShop.ViewModels;

namespace OnlineShop.Services;

public interface IUserEntityService
{
    public Task CreateAsync(string firstName,string lastName,string phone,CancellationToken cancellationToken);
    public Task UpdateAsync(int id,string firstName,string lastName,string phone,CancellationToken cancellationToken);
    public Task DeleteAsync(int id,CancellationToken cancellationToken);
    public Task<UserEntity> GetByIdAsync(int id, CancellationToken cancellationToken);
    public Task<List<UserViewModel>> GetFullNameListAsync(CancellationToken cancellationToken);
    public Task ToggleActivationAsync(int id, CancellationToken cancellationToken);
}
