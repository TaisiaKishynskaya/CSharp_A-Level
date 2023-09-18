using Mod2.Lection5.Hw1.Models;

namespace Mod2.Lection5.Hw1.Services;

public class Logger
{
    private static readonly Logger _instance = new();

    static Logger() { }
    private Logger() { }

    internal List<string> logs = new();

    internal static Logger Instance => _instance;

    readonly FileService fileService = new();

    public void Log(LogType type, string message)
    {
        var logEntry = $"{DateTime.Now}: {type}: {message}";

        logs.Add(logEntry);
        Console.WriteLine(logEntry);

        fileService.WriteLogToFile(string.Join(" ", logs));  // Вызываем WriteLogToFile для записи логов в файл
    }
}
