using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ModuleWork3._1;

internal class ContactBook : IContactRepository
{
    internal ConcurrentDictionary< uint, Contact > contacts = new();

    public void AddToDict()
    {
        var contactInDict = InputContact();
        
        if (contacts.TryAdd(contactInDict.Number, contactInDict))
        {
            Console.WriteLine($"Contact added with number {contactInDict.Number}");
        }
        else
        {
            Console.WriteLine("Failed to add contact. Number already exists.");
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
}
