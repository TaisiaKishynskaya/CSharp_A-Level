namespace Mod3.Lection2.Hw1._2;

internal class Program
{
    static void Main(string[] args)
    {
        // Створюємо об'єкти спостерігачів
        var observer1 = new EventObserver("Спостерігач 1");
        var observer2 = new EventObserver("Спостерігач 2");
        var observer3 = new EventObserver("Спостерігач 3");

        // Створюємо об'єкт виробника подій
        var eventProducer = new EventProducer();

        // Додаємо спостерігачів
        eventProducer.AddObserver(observer1);
        eventProducer.AddObserver(observer2);
        eventProducer.AddObserver(observer3);

        // Запускаємо події
        eventProducer.TriggerEvent("Подія 1");
        eventProducer.TriggerEvent("Подія 2");

        // Використовуємо методи розширення
        var firstObserver = eventProducer.FirstOrDefault2(name => name.Contains("2"));
        Console.WriteLine($"Перший спостерігач, який містить '2': {firstObserver ?? "Не знайдено"}");

        var skippedObservers = eventProducer.SkipWhile2(name => !name.Contains("2"));
        Console.WriteLine("Спостерігачі після першого, який містить '2':");
        foreach (var observer in skippedObservers)
        {
            Console.WriteLine(observer);
        }
    }
}
