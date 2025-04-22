namespace OnlineShop.Helpers;

public static class EncriptionHelper
{
    public static string Encrypt(string str)
    {
        return $"{str}{str}";
    }

    public static string Decrypt(string str)
    {
        return str[..(str.Length / 2)];
    }
}
