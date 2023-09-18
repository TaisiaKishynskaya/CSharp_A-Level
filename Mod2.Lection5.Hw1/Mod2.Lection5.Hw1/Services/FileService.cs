namespace Mod2.Lection5.Hw1.Services;

internal class FileService
{
    private readonly string logDirectory = "LogDirectory"; // Имя каталога для логов

    public FileService()
    {
        // Проверка и создание каталога для логов, если его нет
        if (!Directory.Exists(logDirectory))
        {
            Directory.CreateDirectory(logDirectory);
        }
    }

    public void WriteLogToFile(string logMessage)
    {
        // Создание имени файла на основе текущей даты и времени
        var logFileName = $"{DateTime.Now:MM_dd_yyyy_hh_mm_ss_fff_tt}.txt";
        var logFilePath = Path.Combine(logDirectory, logFileName);

        File.WriteAllText(logFilePath, logMessage);  // Запись лога в файл

        CheckLogFilesCount();  // Проверка количества файлов в каталоге
    }

    private void CheckLogFilesCount()
    {
        var logFiles = Directory.GetFiles(logDirectory);  // Получение всех файлов логов в каталоге

        // Если количество файлов превышает 3, удалить самый старый файл
        if (logFiles.Length > 3)
        {
            var oldestFile = logFiles
                .Select(f => new FileInfo(f))
                .OrderBy(f => f.CreationTime)
                .First();

            oldestFile.Delete();
        }
    }
}

