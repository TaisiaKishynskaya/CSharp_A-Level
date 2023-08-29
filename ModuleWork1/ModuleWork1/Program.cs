string? command;
string? task;
string status;
var dateFormat = "dd.MM.yyyy HH:mm";
const string validChars = "-_ .*";
var col = 3;
var items = new string[1, col];

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
                        MarkItem();
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


string[,] AddTask(string[,] array, string element)
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

void AddItem()
{
    Console.WriteLine("Add task:");

    task = Console.ReadLine();

    if (!string.IsNullOrEmpty(task) &&
            task.All(c => char.IsLetterOrDigit(c) || validChars.Contains(c)))
    {
        var normalizedTask = NormalizeString(task);

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
            items = AddTask(items, task);
            items[items.GetLength(0) - 1, 1] = "0";
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
            task.All(c => char.IsLetterOrDigit(c) || validChars.Contains(c)))
    {
        if (task == "*")
        {
            items = new string[0, col];
            Console.WriteLine("Array cleared.");
        }
        else
        {
            var normalizedTask = NormalizeString(task);

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

void MarkItem()
{
    status = ValidateStatus();

    Console.WriteLine("Input task:");
    task = Console.ReadLine();

    if (!string.IsNullOrEmpty(task) &&
        task.All(c => char.IsLetterOrDigit(c) || validChars.Contains(c)))
    {
        var taskFound = false;

        for (var i = 0; i < items.GetLength(0); i++)
        {
            if (items[i, 0] == task)
            {
                taskFound = true;
                switch (status)
                {
                    case "1":
                        items[i, 1] = status;
                        items[i, 2] = GetDate();
                        break;
                    case "0":
                        items[i, 1] = "0";
                        items[i, 2] = string.Empty;
                        break;
                }

                break;
            }
        }

        if (!taskFound)
        {
            Console.WriteLine("Task not found.");
        }
    }
}

void Show()
{
    status = ValidateStatus();

    if (status == "1" || status == "0")
    {
        Console.WriteLine("Tasks:");
        for (var i = 0; i < items.GetLength(0); i++)
        {
            if (items[i, 1] == status)
            {
                Console.WriteLine($"Task: {items[i, 0]}, Status: {items[i, 1]}, Date: {items[i, 2]}");
            }
        }
    }
}


static string ValidateStatus()
{
    while (true)
    {
        Console.WriteLine("Input status: 0 (not done) or 1 (done):");
        var input = Console.ReadLine();

        if (input != "0" && input != "1")
        {
            Console.WriteLine("Input 0 or 1!");
        }
        else return input;
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

string GetDate()
{
    Console.WriteLine("Press any button to continue OR press 'Enter' to set current date automatically:");

    if (Console.ReadKey().Key == ConsoleKey.Enter)
    {
        var date = DateTime.Now;
        var formattedDate = date.ToString(dateFormat);
        return formattedDate;
    }
    else
    {
        var date = ValidateDateTime().ToString(dateFormat);
        return date;
    }
}

string NormalizeString(string task)
{
    return new string(task.ToLower().Where(c => !char.IsWhiteSpace(c)).ToArray());
}
