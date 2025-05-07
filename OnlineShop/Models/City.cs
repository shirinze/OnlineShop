
using OnlineShop.Enums;

namespace OnlineShop.Models;

public class City
{
    public int CityId { get; set; }
    public string CityName { get; set; } = string.Empty;
    public CityEnum CityEnum { get; set; }

}
