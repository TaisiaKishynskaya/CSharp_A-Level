namespace Mod3.Lection2.Hw1._2;

public static class EventExtensions
{
    public static string FirstOrDefault2(this EventProducer producer, Func<string, bool> predicate)
    {
        foreach (var observer in producer.observers)
        {
            if (predicate(observer.Name))
            {
                return observer.Name;
            }
        }
        return null;
    }

    public static IEnumerable<string> SkipWhile2(this EventProducer producer, Func<string, bool> predicate)
    {
        bool skip = true;
        foreach (var observer in producer.Observers)
        {
            if (!predicate(observer.Name))
            {
                skip = false;
            }
            if (!skip)
            {
                yield return observer.Name;
            }
        }
    }
}