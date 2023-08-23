using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

string? command;
string? task;
int status;
var dateFormat = "dd.MM.yyyy HH:mm";
var col = 2;
var items = new string[1, col];

var doneItems = new string[1];
var notDoneItems = new string[1];
//var row = 5;
//var statusWithItems = new string[row, col];

//AddItemNew();
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

static int ValidateStatus() //переим.
{
    while (true)
    {
        Console.WriteLine("Input status: 0 (not done) or 1 (done):");
        var input = Console.ReadLine();

        if(int.TryParse(input, out var result))
        {
            if (result != 0 && result != 1)
            {
                Console.WriteLine("Input 0 or 1!");
            }
            else return result;
        }
        else
        {
            Console.WriteLine("Input number!");
        }
    }
}

DateTime ValidateDateTime()
{
    while (true)
    {
        Console.WriteLine($"Input date when the task was completed ({dateFormat}):");
        var inputDate = Console.ReadLine();

        if (DateTime.TryParse(inputDate, out var parsedDate))
        {
            return parsedDate;
        }
        else
        {
            Console.WriteLine("Failed to parse date.");
        }

    }
}

string[,] AddTasks(string[,] array, string element)
{
    var rows = array.GetLength(0);
    var cols = array.GetLength(1);

    var newArr = new string[rows + 1, cols];

    for (var i = 0; i < rows; i++)
    {
        for (var j = 0; j < cols; j++)
        {
            newArr[i, j] = array[i, j];
        }
    }

    newArr[rows, 0] = element;

    return newArr;
}

string[,] RemoveTask(string[,] array, int index)
{
    var rows = array.GetLength(0);
    var cols = array.GetLength(1);

    var newArray = new string[rows - 1, cols];

    var newArrayIndex = 0;

    for (var i = 0; i < rows; i++)
    {
        if (i != index)
        {
            for (var j = 0; j < cols; j++)
            {
                newArray[newArrayIndex, j] = array[i, j];
            }
            newArrayIndex++;
        }
    }

    return newArray;
}

string GetDate()
{
    Console.WriteLine("Input date or press 'Enter' to continue:");

    if (Console.ReadKey().Key == ConsoleKey.Enter)
    {
        var date = DateTime.Now;
        var formattedDate = date.ToString(dateFormat);
        return formattedDate;
        //Console.WriteLine(formattedDate);
    }
    else
    {
        var date = ValidateDateTime().ToString();
        return date;
    }
}

void AddItem()
{
    Console.WriteLine("Add task:");

    task = Console.ReadLine();

    if (!string.IsNullOrEmpty(task) &&
            task.All(c => char.IsLetterOrDigit(c) || c == '-' || c == '_' || c == ' ' || c == '.'))
    {
        var normalizedTask = new string(task.ToLower().Where(c => !char.IsWhiteSpace(c)).ToArray());

        var taskAlreadyExists = false;

        for (var i = 0; i < items.GetLength(0); i++)
        {
            var existingTask = items[i, 0];

            if (!string.IsNullOrEmpty(existingTask))
            {
                var normalizedExistingTask = new string(existingTask.ToLower().Where(c => !char.IsWhiteSpace(c)).ToArray());

                if (normalizedExistingTask.Equals(normalizedTask))
                {
                    taskAlreadyExists = true;
                    break;
                }
            }
        }

        if (!taskAlreadyExists)
        {
            items = AddTasks(items, task);
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
        if (task == "*")
        {
            items = new string[0, col]; // Очищаем массив
            Console.WriteLine("Array cleared.");
        }
        else
        {
            var normalizedTask = new string(task.ToLower().Where(c => !char.IsWhiteSpace(c)).ToArray());

            var taskFound = false;

            for (var i = 0; i < items.GetLength(0); i++)
            {
                if (items[i, 0] != null)
                {
                    var normalizedExistingTask = new string(items[i, 0].ToLower().Where(c => !char.IsWhiteSpace(c)).ToArray());

                    if (normalizedExistingTask.Equals(normalizedTask))
                    {
                        items = RemoveTask(items, i);
                        taskFound = true;
                        break;
                    }
                }
            }

            if (!taskFound)
            {
                Console.WriteLine("Task does not exist.");
            }
        }
    }
}


void MarkAs()
{

}

void Show()
{

}

/*void MarkAs()
{
    status = TryParseMethod();

    Console.WriteLine("Input task:");
    task = Console.ReadLine();

    if (!string.IsNullOrEmpty(task) &&
            task.All(c => char.IsLetterOrDigit(c) || c == '-' || c == '_' || c == ' ' || c == '.'))
    {
        var taskAlreadyExists = true;

        for (var i = 0; i < items.Length; i++)
        {
            if (items[i, 0] != null && items[i, 0].Equals(task))
            {
                taskAlreadyExists = false;
                break;
            }
        }

        if (!taskAlreadyExists)
        {
            if (status == 1)
            {
                var date = GetDate();
                var doneTask = status + date + " -- " + task;

                doneItems = AddTasks(doneItems, doneTask);

                notDoneItems = items.Except(doneItems).ToArray();
            }
            else
            {
                var notDoneTask = status + task;
                notDoneItems = AddTasks(notDoneItems, notDoneTask);
            }
        }
        else
        {
            Console.WriteLine("Task is not exists.");
        }
    }
}

void Show()
{
    status = TryParseMethod();

    if (status == 1)
    {
        for (var i = 0; i < doneItems.Length; i++)
        {
            Console.WriteLine(doneItems[i]);
        }
    }
    else
    {
        for (var i = 0; i < notDoneItems.Length; i++)
        {
            Console.WriteLine(notDoneItems[i]);
        }
    }
}*/





/*void AddItemNew()
{
    for(var i = 0; i < row; i++)
    {
        statusWithItems[i, 0] = Console.ReadLine();
        statusWithItems[i, 1] = "status";

    }

    for (var i = 0; i < row; i++)
    {
        Console.WriteLine(statusWithItems[i, 0] + " " + statusWithItems[i, 1] + "Здесь должно быть значение");
    }
}*/