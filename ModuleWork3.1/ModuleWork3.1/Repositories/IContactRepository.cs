namespace ModuleWork3._1.Repositories;

internal interface IContactRepository
{
    Task AddToDictAsync();
    void SearchByFirstName();
    void SearchByLastName();
    void SearchByNumber();
    void PrintContacts();
}
