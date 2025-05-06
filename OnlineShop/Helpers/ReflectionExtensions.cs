namespace OnlineShop.Helpers;

public static class ReflectionExtensions
{
    public static bool HasDataAnnotation<TAttribute>(Type type, string propertyName) where TAttribute : Attribute
    {
        var property = type.GetProperty(propertyName);
        if (property == null) return false;

        return property.GetCustomAttributes(typeof(TAttribute), true).Any();
    }
}