using OnlineShop.Features;
using OnlineShop.Models;

namespace OnlineShop.Specifications;

public class GetUserEntityByContainsNameSpecification:BaseSpecification<UserEntity>
{
    public GetUserEntityByContainsNameSpecification(string? q, OrderType? orderType,int? pageSize,int? pageNumber)
    {
        AddCriteria(x => x.IsActive);

        if (!string.IsNullOrEmpty(q))
        {
            AddCriteria(x => x.FirstName.Contains(q));
        }

        if (orderType != null)
        {
            AddOrderBy(x => x.UserEntityId, orderType.Value);
        }
        if(pageSize.HasValue && pageNumber.HasValue)
        {
            AddPagination(pageSize.Value, pageNumber.Value);
        }
    }
}
