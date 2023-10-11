using System.Collections.Concurrent;
using System.Text;

namespace ModuleWork3._1;

internal class ContactBook : IContactRepository
{
    private readonly string filePath = "contacts.txt";

    internal ConcurrentDictionary< uint, Contact > contacts = new();

    private static SemaphoreSlim fileSemaphore = new SemaphoreSlim(1, 1);

    private FileWatcher fileWatcher; // Для спостереження за файлом

    public ContactBook()
    {
        fileWatcher = new FileWatcher(filePath);
        fileWatcher.FileChanged += OnFileChanged;
    }


    public async Task AddToDictAsync()
    {
        // Захоплюємо семафор для блокування інших процесів
        await fileSemaphore.WaitAsync();

        var contactInDict = InputContact();

        try
        {
            if (contacts.TryAdd(contactInDict.Number, contactInDict))
            {
                Console.WriteLine($"Contact added with number {contactInDict.Number}");

                await SaveToFileAsync(contactInDict);
            }
            else
            {
                Console.WriteLine("Failed to add contact. Number already exists.");
            }
        }
        finally
        {
            // Звільняємо семафор після завершення додавання контакту
            fileSemaphore.Release();
        }
    }

    internal Contact InputContact()
    {
        Contact contactToAdd = new();

        Console.WriteLine("Press any button to input FIRST name :)");
        contactToAdd.FirstName = Console.ReadLine();
        Console.WriteLine($"First name: {contactToAdd.FirstName}");

        Console.WriteLine("Press any button to input LAST name :)");
        contactToAdd.LastName = Console.ReadLine();
        Console.WriteLine($"Last name: {contactToAdd.LastName}");

        contactToAdd.Number = InputValidation.InputNumber();
        Console.WriteLine($"Number: {contactToAdd.Number}");

        Contact copyContactToAdd = new(contactToAdd);

        return copyContactToAdd;
    }


    public void SearchByFirstName()
    {
        var firstName = InputValidation.InputString();
        var searchedFirstNames = contacts.Where(dictValue => dictValue.Value.FirstName == firstName);

        PrintContacts(searchedFirstNames);
    }

    public void SearchByLastName()
    {
        var lastName = InputValidation.InputString();
        var searchedLastNames = contacts.Where(dictValue => dictValue.Value.LastName == lastName);

        PrintContacts(searchedLastNames);
    }

    public void SearchByNumber()
    {
        var number = InputValidation.InputNumber();
        var searchedNumbers = contacts.Where(dictKey => dictKey.Key == number);

        PrintContacts(searchedNumbers);
    }


    public void PrintContacts()
    {
        foreach (var entry in contacts)
        {
            Console.WriteLine($"Number: {entry.Key}, First Name: {entry.Value.FirstName}, Last Name: {entry.Value.LastName}");
        }
    }

    internal void PrintContacts(IEnumerable<KeyValuePair<uint, Contact>> keyValuePairs)
    {
        Console.WriteLine("Found contacts:");

        foreach (var entry in keyValuePairs)
        {
            Console.WriteLine($"Number: {entry.Key}, First Name: {entry.Value.FirstName}, Last Name: {entry.Value.LastName}");
        }
    }


    private async Task SaveToFileAsync(Contact contact)
    {
        var contactData = $"{contact.Number}, {contact.FirstName}, {contact.LastName}\n";

        try
        {
            await fileSemaphore.WaitAsync();

            if (!File.Exists(filePath))
            {
                // Файл не існує, створимо його з пустим вмістом
                File.Create(filePath).Close();
            }
            else
            {
                // Файл існує, виведемо повідомлення про інші процеси
                Console.WriteLine("Another process has already written to the file.");
                return;
            }

            await File.AppendAllTextAsync(filePath, contactData, Encoding.UTF8);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to write to file: {ex.Message}");
        }
        finally
        {
            fileSemaphore.Release();
        }
    }

    private void OnFileChanged(object? sender, string filePath)
    {
        Console.WriteLine($"File {filePath} was changed.");
    }

}
