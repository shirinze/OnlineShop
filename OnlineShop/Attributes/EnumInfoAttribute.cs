namespace OnlineShop.Attributes;

[AttributeUsage(AttributeTargets.Enum)]
public class EnumInfoAttribute(string route):Attribute
{
    public string Route { get; set; } = route;
}
