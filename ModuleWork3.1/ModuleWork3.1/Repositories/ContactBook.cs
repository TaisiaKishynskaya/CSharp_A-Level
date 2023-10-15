using System.Collections.Concurrent;
using System.Text;
using ModuleWork3._1.Models;
using ModuleWork3._1.Services;

namespace ModuleWork3._1.Repositories;

internal class ContactBook : IContactRepository
{
    private readonly string filePath = "contacts.txt";

    internal ConcurrentDictionary<uint, Contact> contacts = new();

    private static SemaphoreSlim fileSemaphore = new(1, 1);

    private FileWatcher _fileWatcher; // Для спостереження за файлом

    public event EventHandler DataUpdated; // Событие для оповещения об обновлении данных


    public ContactBook()
    {
        _fileWatcher = new FileWatcher(filePath);
        _fileWatcher.FileChanged += OnFileChanged;
    }


    public async Task AddToDictAsync()
    {

        await fileSemaphore.WaitAsync();
        Console.WriteLine("Semaphore is closed");
        var contactInDict = InputContact();

        try
        {
            if (contacts.TryAdd(contactInDict.Number, contactInDict))
            {
                Console.WriteLine($"Contact added with number {contactInDict.Number}");
                await SaveToFileAsync(contactInDict);
                OnDataUpdated(EventArgs.Empty); // Сообщаем о обновлении данных
            }
            else
            {
                Console.WriteLine("Failed to add contact. Number already exists.");
            }
        }
        finally
        {
            Console.WriteLine("Semaphore is opened");
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
        var firstNamePrefix = InputValidation.InputString();
        var searchedFirstNames = contacts.Where(dictValue => dictValue.Value.FirstName.StartsWith(firstNamePrefix, StringComparison.OrdinalIgnoreCase));

        PrintContacts(searchedFirstNames);
    }

    public void SearchByLastName()
    {
        var lastNamePrefix = InputValidation.InputString();
        var searchedLastNames = contacts.Where(dictValue => dictValue.Value.LastName.StartsWith(lastNamePrefix, StringComparison.OrdinalIgnoreCase));

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
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            await File.AppendAllTextAsync(filePath, contactData, Encoding.UTF8);

            OnDataUpdated(EventArgs.Empty); // Сообщаем о обновлении данных
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to write to file: {ex.Message}");
        }
    }

    protected virtual void OnDataUpdated(EventArgs e)
    {
        DataUpdated?.Invoke(this, e);
    }

    private void OnFileChanged(object? sender, string filePath)
    {
        Console.WriteLine($"File {filePath} was changed.");
    }

}
