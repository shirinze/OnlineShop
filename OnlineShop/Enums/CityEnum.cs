using OnlineShop.Attributes;
using System.ComponentModel;

namespace OnlineShop.Enums;

[EnumInfo("/CityEnums")]
public enum CityEnum
{
    [Description("تهران")]
    [Info("textColor", "#000000")]
    [Info("backgroundColor", "#FFFFFF")]
    [Info("borderColor", "#CCCCCC")]
    Tehran = 1,

    [Description("اصفهان")]
    [Info("textColor", "#FFFFFF")]
    [Info("backgroundColor", "#123456")]
    [Info("borderColor", "#654321")]
    Isfahan = 2,

    [Description("شیراز")]
    [Info("borderColor", "#654321")]
    Shiraz = 3,

    [Description("مشهد")]
    Mashhad = 4,

    [Description("تبریز")]
    Tabriz = 5

}
