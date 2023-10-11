namespace ModuleWork3._1;

internal interface IContactRepository
{
    Task AddToDictAsync();
    void SearchByFirstName();
    void SearchByLastName();
    void SearchByNumber();
    void PrintContacts();
}
