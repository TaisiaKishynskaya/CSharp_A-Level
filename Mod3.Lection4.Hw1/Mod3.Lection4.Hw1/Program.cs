using System.IO;

namespace Mod3.Lection4.Hw1;

internal class Program
{
    async static Task Main()
    {
        var helloContentTask = ReadFromHelloFile();
        var worldContentTask = ReadFromWorldFile();

        // Ожидаем завершения обоих асинхронных методов
        await Task.WhenAll(helloContentTask, worldContentTask);

        // Получаем результаты
        var helloContent = await helloContentTask;
        var worldContent = await worldContentTask;

        Console.WriteLine(helloContent + worldContent);
    }

    async static Task<string> ReadFromHelloFile()
    {
        try
        {
            return await Task.Run(() => ReadFile("hello.txt"));
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("hello.txt not found.");
            return string.Empty;
        }
    }

    async static Task<string> ReadFromWorldFile()
    {
        try
        {
            return await Task.Run(() => ReadFile("world.txt"));
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("world.txt not found.");
            return string.Empty;
        }
    }

    /*async static Task ReadFromWorldFile()
    {
        try
        {
            var content = ReadFile("world.txt");
            Console.WriteLine($"File content: {content}");
            return content;
        }
        catch (FileNotFoundException)
        {
            return "world.txt not found.";
        }
    }*/

    static string ReadFile(string path)
    {
        // Класс StreamReader для считывания данных из файла
        using var reader = new StreamReader(path);
        return reader.ReadToEnd(); // Считываем все содержимое файла
    }
}
