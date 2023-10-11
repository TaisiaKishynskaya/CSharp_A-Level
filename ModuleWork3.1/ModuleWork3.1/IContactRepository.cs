using System.Collections.Concurrent;

namespace ModuleWork3._1;

internal interface IContactRepository
{
    void AddToDict();
    void SearchByFirstName();
    void SearchByLastName();
    void SearchByNumber();
    void PrintContacts();
}
