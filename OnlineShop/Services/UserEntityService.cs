using Microsoft.Extensions.Caching.Memory;
using OnlineShop.Data;
using OnlineShop.Exceptions;
using OnlineShop.Features;
using OnlineShop.Helpers;
using OnlineShop.Models;
using OnlineShop.Specifications;
using OnlineShop.ViewModels;


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

    public async Task<UserViewModel> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var value = memoryCache.Get<UserEntity>(id);

        if (value is null)
        {
            value = await unitOfWork.UserEntityRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new NotFoundException($"entity with id {id} not found.");

            memoryCache.Set(id, value, DateTime.Now.AddSeconds(30));
        }

        return value.ToViewModel();
    }

    public async Task<PaginationResult<UserViewModel>> GetListAsync(string? q,
        OrderType? orderType
        ,int? pageSize
        ,int? pageNumber
        , CancellationToken cancellationToken
        )
    {
       

        var cacheKey = $"user_list_{q}_{orderType}_{pageSize}_{pageNumber}";
        var cachedResult = memoryCache.Get<PaginationResult<UserViewModel>>(cacheKey);

        if (cachedResult != null)
        {
            return cachedResult;
        }

        var specification = new GetUserEntityByContainsNameSpecification(q, orderType, pageSize, pageNumber);
        var (totalCount, entities) = await unitOfWork.UserEntityRepository.GetAllAsync(specification, cancellationToken);
        var viewModels = entities.ToViewModel();
        var result = PaginationResult<UserViewModel>.Create(pageSize??0, pageNumber??0, totalCount, viewModels);

        memoryCache.Set(cacheKey, result, DateTime.Now.AddSeconds(30));

        return result;


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
