using System;

namespace Mod3.Lection5.Hw1._2;

internal class Program
{
    private static List<string> urls = new(){
        "https://www.csharptutorial.net/csharp-concurrency/csharp-thread/",
        "https://www.csharptutorial.net/csharp-concurrency/csharp-task/"
    };

    private static readonly List<string> files = new() { "thread.txt", "task.txt" };

    static async Task Main()
    {
        Program program = new();
        await program.WriteToFilesAsync();
        await program.AnalyzeFilesAsync(files);
    }


    private async Task<string> ReadFromUrlAsync(string url)
    {
        using var httpClient = new HttpClient();

        try
        {
            // Выполняем GET-запрос к URL страницы
            var response = await httpClient.GetAsync(url);

            // Проверяем, был ли запрос успешным (статус код 200-299)
            if (response.IsSuccessStatusCode)
            {
                // Получаем содержимое ответа в виде строки (HTML-код страницы)
                var htmlContent = await response.Content.ReadAsStringAsync();

                return htmlContent;
            }
            else
            {
                Console.WriteLine($"HTTP Error: {response.StatusCode}");
                return string.Empty;
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"HTTP Request Error: {e.Message}");
            return string.Empty;
        }
    }

    

    private async Task WriteToFilesAsync()
    {
        Program program = new();

        var tasks = new List<Task<string>>();
        foreach (var url in urls)
        {
            tasks.Add(program.ReadFromUrlAsync(url));
        }

        var htmlContents = await Task.WhenAll(tasks);

        for (var i = 0; i < urls.Count; i++)
        {
            var fileIndex = i % files.Count; // Чтобы выбирать файлы по кругу
            var filePath = files[fileIndex];

            program.WriteInFileAsync(filePath, htmlContents[i]);
        }
    }

    private async void WriteInFileAsync(string path, string content)
    {
        try
        {
            // Записываем строку в файл, перезаписывая существующий файл или создавая новый
            await File.WriteAllTextAsync(path, content);

            Console.WriteLine("Successful writing to file.");
        }
        catch (IOException e)
        {
            Console.WriteLine($"Error with writing to file: {e.Message}");
        }
    }


    private async static Task<string> ReadFromFileAsync()
    {
        var content = string.Empty;

        foreach (var file in files)
        {
            try
            {
                content = await Task.Run(() => ReadFileAsync(file));
                break; // Если файл найден, выходим из цикла
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"{file} not found.");
            }
        }

        return content; // Возвращаем значение вне цикла
    }

    static async Task<string> ReadFileAsync(string path)
    {
        using var reader = new StreamReader(path);  // Clas StreamReader for reading from files
        return await reader.ReadToEndAsync(); // Read all from files
    }
    

    private async Task AnalyzeFilesAsync(List<string> filePaths)
    {
        var contentTasks = new List<Task<string>>();

        foreach (var file in files)
        {
            contentTasks.Add(ReadFromFileAsync()); 
        }

        await Task.WhenAll(contentTasks);

        var wordCounts = CountWordOccurrences(contentTasks);
        var uniqueWords = FindUniqueWords(await wordCounts);

        PrintWordCounts(await wordCounts);

        Console.WriteLine($"\n---------------------------------------------------------------------------\n");

        PrintUniqueWords(uniqueWords);
    }

    private async Task<Dictionary<string, int>> CountWordOccurrences(List<Task<string>> contentTasks)
    {
        var wordCounts = new Dictionary<string, int>();

        foreach (var contentTask in contentTasks)
        {
            var fileContent = await contentTask;

            if (!string.IsNullOrEmpty(fileContent))
            {
                var lines = fileContent.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var line in lines)
                {
                    var words = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var word in words)
                    {
                        // Подсчитываем количество каждого слова
                        if (wordCounts.ContainsKey(word))
                            wordCounts[word]++;
                        else
                            wordCounts[word] = 1;
                    }
                }
            }
        }

        return wordCounts;
    }

    private HashSet<string> FindUniqueWords(Dictionary<string, int> wordCounts)
    {
        var uniqueWords = new HashSet<string>(wordCounts.Keys);
        return uniqueWords;
    }

    private void PrintWordCounts(Dictionary<string, int> wordCounts)
    {
        Console.WriteLine("Word Counts in Both Files:");
        foreach (var kvp in wordCounts)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
    }

    private void PrintUniqueWords(HashSet<string> uniqueWords)
    {
        Console.WriteLine("\nUnique Words in Both Files:");
        foreach (var word in uniqueWords)
        {
            Console.WriteLine(word);
        }
    }
}