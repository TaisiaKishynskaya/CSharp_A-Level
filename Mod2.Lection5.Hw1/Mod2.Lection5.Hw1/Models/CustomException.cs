namespace Mod2.Lection5.Hw1.Models;

internal class CustomException : Exception
{
    public CustomException() { }

    public CustomException(string message) : base(message) { }
}
