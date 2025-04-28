using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OnlineShop.Exceptions;
using OnlineShop.Models;
using OnlineShop.ViewModels;

namespace OnlineShop.Services;

public class UserEntityService(OnlineShopDBContext db,IMemoryCache memoryCache) : IUserEntityService
{
    public async Task CreateAsync(string firstName, string lastName, string phone, CancellationToken cancellationToken)
    {
        var value = UserEntity.Create(firstName,lastName,phone);
        await db.AddAsync(value, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var value = await db.Set<UserEntity>().FirstOrDefaultAsync(x => x.UserEntityId == id, cancellationToken) ?? throw new NotFoundException($"entity with id {id} not found.");
        db.Remove(value);
        await db.SaveChangesAsync(cancellationToken);
        memoryCache.Remove(id);
    }

    public async Task<UserEntity> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var value = memoryCache.Get<UserEntity>(id);

        if (value is null)
        {
            value = await db.Set<UserEntity>().FirstOrDefaultAsync(a => a.UserEntityId == id, cancellationToken)
                ?? throw new NotFoundException($"entity with id {id} not found.");

            memoryCache.Set(id, value, DateTime.Now.AddSeconds(30));
        }

        return value;
    }

    public async Task<List<UserViewModel>> GetFullNameListAsync(CancellationToken cancellationToken)
    {
        var users = await db.UserEntities.Select(x => new
        {
            x.FirstName,
            x.LastName
            

        }).ToListAsync(cancellationToken);
        var result = users.Select(x => new UserViewModel
        {
            FullName = x.FirstName + " " + x.LastName
        }).Where(u => u.FullName.Contains('q')).ToList();
        return result;
    }

    public async Task ToggleActivationAsync(int id, CancellationToken cancellationToken)
    {
        var value = await db.Set<UserEntity>().FirstOrDefaultAsync(a => a.UserEntityId == id, cancellationToken)
            ?? throw new NotFoundException($"entity with id {id} not found.");

        value.Update(value.FirstName,value.LastName,value.Phone,!value.IsActive);
        await db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(int id, string firstName, string lastName, string phone, CancellationToken cancellationToken)
    {
        var value = await db.Set<UserEntity>().FirstOrDefaultAsync(x => x.UserEntityId == id, cancellationToken) ?? throw new NotFoundException($"entity with id {id} not found ");
        
        value.Update(firstName, lastName, phone,value.IsActive);
        await db.SaveChangesAsync(cancellationToken);
        memoryCache.Remove(id);
    }
}
