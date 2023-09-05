using Mod2.Lection3.Hw1.Models;

namespace Mod2.Lection3.Hw1;

internal class Candy : Sugar, ISwitnessLook
{
    public string? ChocolateType { get; set; }
    public string? NameSweetness { get; set; }
    public string? TypeWrapper { get; set; }
    public string? ColorWrapper { get; set; }
}
