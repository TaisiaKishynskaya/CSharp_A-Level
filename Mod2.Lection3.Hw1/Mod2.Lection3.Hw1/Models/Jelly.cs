using Mod2.Lection3.Hw1.Models;

namespace Mod2.Lection3.Hw1;

internal class Jelly : Sugar
{
    public string? GellingAgents { get; set; }

    public Jelly(string name, double weight, string taste, string color, string sugar, string agent, string wrapperColor)
        : base(name, weight, taste, color, sugar, wrapperColor)
    {
        GellingAgents = agent;
    }
}
