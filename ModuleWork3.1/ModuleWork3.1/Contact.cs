namespace ModuleWork3._1;

internal class Contact : IContact
{
    private string? _firstName;
    private string? _lastName;

    public uint Number { get; set; }

    public string? FirstName
    {
        get => _firstName;
        set
        {
            _firstName = InputValidation.InputString();
        }
    }

    public string? LastName
    {
        get => _lastName;
        set
        {
            _lastName = InputValidation.InputString();
        }
    }

    internal Contact() { }
    internal Contact(string FirstName, string LastName) { }
    internal Contact(Contact other) 
    {
        _firstName = other.FirstName;
        _lastName = other.LastName;
        Number = other.Number;
    }
}
