using System.Collections.Concurrent;
using ModuleWork3._1.Repositories;

namespace ModuleWork3._1;

internal class Program
{
    private static void Main()
    {
        IContactRepository contactRepository = new ContactBook();
        var app = new App(contactRepository);
        app.MainMenu();

    }
}