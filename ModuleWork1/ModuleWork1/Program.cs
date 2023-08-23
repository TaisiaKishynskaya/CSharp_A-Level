string? command;
string? task;
var items = new string[1];

InputCommand();


void InputCommand() 
{
    while (true)
    {
        Console.WriteLine("Input one of this command: add-item, remove-item, mark-as, show, exit.");
        command = Console.ReadLine();

        string[] commandsArr = { "add-item", "remove-item", "mark-as", "show", "exit" };

        for (var i = 0; i < commandsArr.Length; i++)
        {
            if (command == commandsArr[i])
            {
                switch (commandsArr[i])
                {
                    case "add-item":
                        Console.WriteLine("A");
                        AddItem();
                        break;
                    case "remove-item":
                        Console.WriteLine("R");
                        RemoveItem();
                        break;
                    case "mark-as":
                        Console.WriteLine("M");
                        MarkAs();
                        break;
                    case "show":
                        Console.WriteLine("S");
                        Show();
                        break;
                    case "exit":
                        return;
                }
            }
        }
    }
}

static string[] AddElement(string[] array, string element)
{
    var newArr = new string[array.Length + 1];
    Array.Copy(array, newArr, array.Length);
    newArr[array.Length] = element;
    return newArr;
}

static string[] RemoveElement(string[] array, int index)
{
    var newArr = new string[array.Length - 1];
    Array.Copy(array, 0, newArr, 0, index);
    Array.Copy(array, index + 1, newArr, index, array.Length - index - 1);
    return newArr;
}

void AddItem()
{
    Console.WriteLine("Add task:");

    task = Console.ReadLine();

    if (!string.IsNullOrEmpty(task) &&
            task.All(c => char.IsLetterOrDigit(c) || c == '-' || c == '_' || c == ' ' || c == '.'))
    {
        var taskAlreadyExists = false;

        for (var i = 0; i < items.Length; i++)
        {
            if (items[i] != null && items[i].Equals(task))
            {
                taskAlreadyExists = true;
                break;
            }
        }

        if (!taskAlreadyExists)
        {
            items = AddElement(items, task);
        }
        else
        {
            Console.WriteLine("Task already exists.");
        }
    }
}

void RemoveItem()
{
    Console.WriteLine("Input the task to remove or '*' to remove all tasks:");

    task = Console.ReadLine();

    if (!string.IsNullOrEmpty(task) &&
            task.All(c => char.IsLetterOrDigit(c) || c == '-' || c == '_' || c == ' ' || c == '.' || c == '*'))
    {
        var taskAlreadyExists = true;

        for (var i = 0; i < items.Length; i++)
        {
            if (items[i] != null && items[i].Equals(task))
            {
                taskAlreadyExists = false;
                break;
            }
        }

        if (task == "*")
        {
            var newArr = new string[1];
            items = newArr;

            Console.WriteLine("Array cleared.");
        }
        else if (!taskAlreadyExists)
        {
            var indexToRemove = Array.FindIndex(items, item => item == task);
            items = RemoveElement(items, indexToRemove);
        }
        else
        {
            Console.WriteLine("Task is not exists.");
        }
    }
}

void MarkAs()
{
    
}

void Show()
{
    
}
