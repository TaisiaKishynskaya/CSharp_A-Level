namespace Mod3.Lection2.Hw1._2;

internal class EventProducer
{
    private List<IObserver> observers = new();

    public void AddObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObservers(string message)
    {
        foreach (var observer in observers)
        {
            observer.Update(message);
        }
    }

    public void TriggerEvent(string message)
    {
        Console.WriteLine($"Подія: {message}");
        NotifyObservers(message);
    }
}
