using AutoMapper;
using OnlineShop.Models;
using OnlineShop.ViewModels;

namespace OnlineShop.Helpers;

public static class MapperHelper
{
    public static UserViewModel ToViewModel(this UserEntity entity)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<UserEntity, UserViewModel>();
        });
        var mapper = new Mapper(config);
        return mapper.Map<UserViewModel>(entity);
    }

    public static List<UserViewModel> ToViewModel(this List<UserEntity> entities)
    {
        return entities.Select(x => new UserViewModel
        {
            UserEntityId = x.UserEntityId,
            FirstName = x.FirstName,
            LastName = x.LastName,
            IsActive = x.IsActive
        }).ToList();
    }
}
