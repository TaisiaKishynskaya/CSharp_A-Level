﻿using System.Collections.Concurrent;

namespace ModuleWork3._1;

internal class Program
{
    static void Main()
    {
        IContactRepository contactRepository = new ContactBook();
        var app = new App(contactRepository);
        app.MainMenu();
    }
}