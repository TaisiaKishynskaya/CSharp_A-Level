﻿using System.Collections.Concurrent;
using ModuleWork3._1.Repositories;
using ModuleWork3._1.Services;

namespace ModuleWork3._1;

internal class App
{
    private readonly IContactBookRepository _contactRepository;

    public App(IContactBookRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }


    internal async void MainMenu()
    {
        while (true)
        {
            Console.WriteLine("Input number 1-3, what you want to do:\n  1 - add contact\n  2 - search contacts\n  3 - view all contacts");

            var choise = InputValidationService.InputChoise();

            switch (choise)
            {
                case "1":
                    await _contactRepository.AddToDictAsync();
                    break;
                case "2":
                    SearchMenu();
                    break;
                case "3":
                    _contactRepository.PrintContacts();
                    break;
            }
        }
    }

    internal void SearchMenu()
    {
        while (true)
        {
            Console.WriteLine("Input number 1-3, how do you want to search contacts:\n  1 - search by first name\n  2 - search ny last name\n  3 - search by number");

            var choise = InputValidationService.InputChoise();

            switch (choise)
            {
                case "1":
                    _contactRepository.SearchByFirstName();
                    break;
                case "2":
                    _contactRepository.SearchByLastName();
                    break;
                case "3":
                    _contactRepository.SearchByNumber();
                    break;
            }
        }
    }
}
