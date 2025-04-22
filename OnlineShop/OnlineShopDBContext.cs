using Microsoft.EntityFrameworkCore;
using OnlineShop.Helpers;
using OnlineShop.Models;
namespace OnlineShop;

public class OnlineShopDBContext(DbContextOptions options):DbContext(options)
{
    public DbSet<UserEntity> UserEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasKey(x=>x.UserEntityId);
        modelBuilder.Entity<UserEntity>().Property(x => x.FirstName).HasMaxLength(50);
        modelBuilder.Entity<UserEntity>().Property(x => x.LastName).HasMaxLength(50);
        modelBuilder.Entity<UserEntity>().Property(x => x.Phone).HasMaxLength(20);
        modelBuilder.Entity<UserEntity>().Property(x=>x.Phone).HasConversion(v=>EncriptionHelper.Encrypt(v.ToString()),
            v=>EncriptionHelper.Decrypt(v.ToString()));


        base.OnModelCreating(modelBuilder);
    }
}
