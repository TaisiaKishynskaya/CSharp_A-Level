using System.IO;

namespace Mod3.Lection4.Hw1;

internal class Program
{
    private static readonly List<string> files = new() { "hello.txt", "world.txt" };

    async static Task Main()
    {
        var helloContentTask = ReadFromHelloFile();
        var worldContentTask = ReadFromWorldFile();

        // Wait the end of both Asynchronous methods
        await Task.WhenAll(helloContentTask, worldContentTask);

        // Get results
        var helloContent = await helloContentTask;
        var worldContent = await worldContentTask;

        Console.WriteLine(helloContent + worldContent);
    }

    async static Task<string> ReadFromHelloFile()
    {
        try
        {
            return await Task.Run(() => ReadFile(files[0]));
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"{files[0]} not found.");
            return string.Empty;
        }
    }

    async static Task<string> ReadFromWorldFile()
    {
        try
        {
            return await Task.Run(() => ReadFile(files[1]));
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"{files[1]} not found.");
            return string.Empty;
        }
    }

    static string ReadFile(string path)
    {
        using var reader = new StreamReader(path);  // Clas StreamReader for reading from files
        return reader.ReadToEnd(); // Read all from files
    }
}
