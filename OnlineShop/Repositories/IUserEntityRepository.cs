using OnlineShop.Models;

namespace OnlineShop.Repositories;
public interface IUserEntityRepository
{
    public Task AddAsync(UserEntity userEntity,CancellationToken cancellationToken);
    public void Update(UserEntity userEntity);
    public void Delete(UserEntity userEntity);
    public Task<UserEntity> GetByIdAsync(int id,CancellationToken cancellationToken);
    public Task<List<UserEntity>> GetAllAsync(string? q,CancellationToken cancellationToken);
}

