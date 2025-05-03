using OnlineShop.Models;

namespace OnlineShop.Repositories;

public interface ICityRepository
{
    public Task<List<City>> GetListCityAsync(CancellationToken cancellationToken);
}
