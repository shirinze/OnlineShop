using OnlineShop.Repositories;

namespace OnlineShop.Data;

public interface IUnitOfWork
{
    public Task CommitAsync(CancellationToken cancellationToken);

    public IUserEntityRepository UserEntityRepository { get; init; }
}
