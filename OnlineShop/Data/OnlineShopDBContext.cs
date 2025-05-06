using Microsoft.EntityFrameworkCore;
using OnlineShop.Helpers;
using OnlineShop.Models;
using System.ComponentModel.DataAnnotations;
namespace OnlineShop.Data;

public class OnlineShopDBContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<UserEntity> UserEntities { get; set; }
    public DbSet<City> Cities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("sr");

        modelBuilder.HasSequence<int>("Increase3By3", "sr").StartsAt(10).IncrementsBy(3);

        #region User
        modelBuilder.Entity<UserEntity>().HasKey(x => x.UserEntityId);
        modelBuilder.Entity<UserEntity>().Property(x => x.UserEntityId)
            .HasDefaultValueSql("NEXT VALUE FOR sr.Increase3By3");

        if (ReflectionExtensions.HasDataAnnotation<RequiredAttribute>(typeof(UserEntity), "FirstName"))
        {
            modelBuilder.Entity<UserEntity>().Property(p => p.FirstName).IsRequired();
        }

        modelBuilder.Entity<UserEntity>().Property(x => x.FirstName).HasMaxLength(50);
        modelBuilder.Entity<UserEntity>().Property(x => x.LastName).HasMaxLength(50);
        modelBuilder.Entity<UserEntity>().Property(x => x.Phone).HasMaxLength(20);
        modelBuilder.Entity<UserEntity>().Property(x => x.Phone)
            .HasConversion(
                v => EncriptionHelper.Encrypt(v.ToString()),
                v => EncriptionHelper.Decrypt(v.ToString())
            );

        modelBuilder.Entity<UserEntity>().Property<DateTime>("CreatedAt").IsRequired();
        modelBuilder.Entity<UserEntity>().Property<DateTime?>("UpdatedAt");
        modelBuilder.Entity<UserEntity>().Property<DateTime?>("DeletedAt");
        modelBuilder.Entity<UserEntity>().Property<bool>("IsDeleted").HasDefaultValue(false);

        modelBuilder.Entity<UserEntity>().HasQueryFilter(x => EF.Property<bool>(x, "IsDeleted") == false);

        #endregion

        #region City
        modelBuilder.Entity<City>().HasKey(x => x.CityId);
        modelBuilder.Entity<City>().Property(x => x.CityName).HasMaxLength(50);
        modelBuilder.Entity<City>().HasData(
            [
                new City { CityId = 1, CityName = "Tehran"},
                new City { CityId = 2, CityName = "Mashhad"},
                new City { CityId = 3, CityName = "Tabriz"},
                new City { CityId = 4, CityName = "Uromiyeh"},
                new City { CityId = 5, CityName = "Ahvaz"},

            ]);
        #endregion


        base.OnModelCreating(modelBuilder);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetCreatedAt();
        SetUpdatedAt();
        SetIsDeleted();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void SetCreatedAt()
    {
        var entries = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);
        foreach (var entry in entries)
        {
            if (entry.Metadata.FindProperty("CreatedAt") != null)
            {
                entry.Property("CreatedAt").CurrentValue = DateTime.Now;
            }
        }
    }
    private void SetUpdatedAt()
    {
        var entries = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            if (entry.Metadata.FindProperty("UpdatedAt") != null)
            {
                entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
            }
        }
    }
    private void SetIsDeleted()
    {
        var entries = ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted);

        foreach (var entry in entries)
        {
            if (entry.Metadata.FindProperty("IsDeleted") != null)
            {
                entry.State = EntityState.Modified;
                entry.Property("IsDeleted").CurrentValue = true;
                entry.Property("DeletedAt").CurrentValue = DateTime.Now;

            }
        }
    }
   
}
