using OnlineShop.Repositories;

namespace OnlineShop.Data;

public class UnitOfWork(OnlineShopDBContext db, IUserEntityRepository userEntityRepository,ICityRepository cityRepository) : IUnitOfWork
{
    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await db.SaveChangesAsync(cancellationToken);
    }
    public IUserEntityRepository UserEntityRepository { get; init; } = userEntityRepository;
    public ICityRepository CityRepository { get; init; } = cityRepository;
}
