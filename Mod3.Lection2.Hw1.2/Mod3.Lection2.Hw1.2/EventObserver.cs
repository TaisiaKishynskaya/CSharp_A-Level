namespace Mod3.Lection2.Hw1._2;

public class EventObserver : IObserver
{
    public string Name { get; }

    public EventObserver(string name)
    {
        Name = name;
    }

    public void Update(string message)
    {
        Console.WriteLine($"{Name} отримав повідомлення: {message}");
    }
}