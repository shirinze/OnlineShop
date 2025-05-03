using Microsoft.Extensions.Caching.Memory;
using OnlineShop.Data;
using OnlineShop.Exceptions;
using OnlineShop.Models;


namespace OnlineShop.Services;

public class UserEntityService(IUnitOfWork unitOfWork,IMemoryCache memoryCache) : IUserEntityService
{
    public async Task CreateAsync(string firstName, string lastName, string phone, CancellationToken cancellationToken)
    {
        var value = UserEntity.Create(firstName,lastName,phone);
        await unitOfWork.UserEntityRepository.AddAsync(value, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var value = await unitOfWork.UserEntityRepository.GetByIdAsync(id,cancellationToken)
            ?? throw new NotFoundException($"entity with id {id} not found.");
        unitOfWork.UserEntityRepository.Delete(value);
        await unitOfWork.CommitAsync(cancellationToken);
        memoryCache.Remove(id);
    }

    public async Task<UserEntity> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var value = memoryCache.Get<UserEntity>(id);

        if (value is null)
        {
            value = await unitOfWork.UserEntityRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new NotFoundException($"entity with id {id} not found.");

            memoryCache.Set(id, value, DateTime.Now.AddSeconds(30));
        }

        return value;
    }

    public async Task<List<UserEntity>> GetListAsync(string? q, CancellationToken cancellationToken)
    {
        var cacheKey = $"user_list_{q}";

        var values = memoryCache.Get<List<UserEntity>>(cacheKey);

        if (values == null)
        {
            values = await unitOfWork.UserEntityRepository.GetAllAsync(q, cancellationToken);
            memoryCache.Set(cacheKey, values, DateTime.Now.AddSeconds(30));
        }

        return values;
    }

    public async Task ToggleActivationAsync(int id, CancellationToken cancellationToken)
    {
        var value = await unitOfWork.UserEntityRepository.GetByIdAsync(id,cancellationToken)
            ?? throw new NotFoundException($"entity with id {id} not found.");

        value.Update(value.FirstName,value.LastName,value.Phone,!value.IsActive);
        unitOfWork.UserEntityRepository.Update(value);
        await unitOfWork.CommitAsync(cancellationToken);
    }

    public async Task UpdateAsync(int id, string firstName, string lastName, string phone, CancellationToken cancellationToken)
    {
        var value = await unitOfWork.UserEntityRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException($"entity with id {id} not found.");
        
        value.Update(firstName, lastName, phone,value.IsActive);
        unitOfWork.UserEntityRepository.Update(value);
        await unitOfWork.CommitAsync(cancellationToken);
        memoryCache.Remove(id);
    }
}
