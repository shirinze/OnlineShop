using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Repositories;

public class CityRepository(OnlineShopDBContext db) : ICityRepository
{
    public async Task<List<City>> GetListCityAsync(CancellationToken cancellationToken)
    {
        return await db.Cities.ToListAsync(cancellationToken);
    }
}
