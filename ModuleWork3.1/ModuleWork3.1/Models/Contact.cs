﻿using ModuleWork3._1.Services;

namespace ModuleWork3._1.Models;

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
            _firstName = InputValidationService.InputString();
        }
    }

    public string? LastName
    {
        get => _lastName;
        set
        {
            _lastName = InputValidationService.InputString();
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
