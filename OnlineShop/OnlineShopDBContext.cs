using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;
namespace OnlineShop;

public class OnlineShopDBContext(DbContextOptions options):DbContext(options)
{
    public DbSet<UserEntity> UserEntities { get; set; }
}
