using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModuleWork3._1
{
    internal class FileWatcher
    {
        private readonly string filePath;
        private readonly SemaphoreSlim fileSemaphore = new(1, 1);

        public event EventHandler<string> FileChanged;

        public FileWatcher(string filePath)
        {
            this.filePath = filePath;
            this.FileChanged = delegate { }; // Пустий обробник подій
        }

        public void StartWatching()
        {
            using var watcher = new FileSystemWatcher
            {
                Path = Path.GetDirectoryName(filePath),
                Filter = Path.GetFileName(filePath),
                EnableRaisingEvents = true,
                NotifyFilter = NotifyFilters.LastWrite
            };

            watcher.Changed += OnFileChanged;

            Console.WriteLine($"File watcher started for file: {filePath}. Waiting for changes...");
            Console.ReadLine();
        }

        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            // Під час зміни файлу читаємо його вміст
            var fileContents = ReadFile();
            Console.WriteLine("File changed. New content:");
            Console.WriteLine(fileContents);

            FileChanged?.Invoke(this, filePath);
        }

        private string ReadFile()
        {
            try
            {
                using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var reader = new StreamReader(fileStream);
                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read file: {ex.Message}");
                return string.Empty;
            }
        }
    }
}
