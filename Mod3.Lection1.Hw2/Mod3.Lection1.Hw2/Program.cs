namespace Mod3.Lection1.Hw2;

internal class Program
{
    static void Main()
    {
        var list = new DoublyLinkedList<int> { 1, 2, 3 };

        // Используем индексатор
        Console.WriteLine(list[0]); // Выводит 1
        Console.WriteLine(list[1]); // Выводит 2
        Console.WriteLine(list[2]); // Выводит 3

        // Удаляем элемент
        list.Remove(2);
        Console.WriteLine(list[1]); // Выводит 3

        // Используем итератор для перебора элементов
        foreach (var item in list)
        {
            Console.WriteLine(item);
        }
    }
}