namespace Mod3.Lection2.Hw2._13;

internal class Program
{
    static void Main()
    {
        Program program = new();

        // Generate a collection of user objects
        var listOfUsers = GenerateUsers();

        program.Task1(listOfUsers);
        program.Task2(listOfUsers);
        program.Task3(listOfUsers);
        program.Task4(listOfUsers);
    }

    private void Task1(List<User> users)
    {
        // Task 1: Find users older than 18 and project their details
        var adults = users
            .Where(user => CalculateAge(user.DateOfBirth) > 18)
            .Select(user => new
            {
                FullName = $"{user.FirstName} {user.LastName}",
                user.DateOfBirth,
                Age = CalculateAge(user.DateOfBirth)
            });

        Console.WriteLine("Users older than 18:");
        foreach (var adult in adults)
        {
            Console.WriteLine($"Name: {adult.FullName}, Date of Birth: {adult.DateOfBirth.ToShortDateString()}, Age: {adult.Age} years");
        }
    }

    private void Task2(List<User> users)
    {
        // Task 2: Group users by email domain and find the most used domain
        var emailDomains = users
            .GroupBy(user => user.Email?.Split('@')[1])
            .Select(group => new
            {
                Domain = group.Key,
                Count = group.Count()
            })
            .OrderByDescending(x => x.Count)
            .FirstOrDefault();

        Console.WriteLine($"\nMost used email domain: {emailDomains?.Domain}, Count: {emailDomains?.Count}");
    }

    private void Task3(List<User> users)
    {
        // Task 3: Convert the collection into an optimized dictionary for search by UserId
        var userDictionary = users.ToDictionary(user => user.UserId);

        var searchUserId = 5; // Example search UserId
        if (userDictionary.TryGetValue(searchUserId, out var searchedUser))
        {
            Console.WriteLine($"\nUser with UserId {searchUserId}: {searchedUser.FirstName} {searchedUser.LastName}");
        }
        else
        {
            Console.WriteLine($"\nUser with UserId {searchUserId} not found.");
        }
    }

    private void Task4(List<User> users)
    {
        // Task 4: Group users by Last Name and project possible relatives
        var possibleRelatives = users
            .GroupBy(user => user.LastName)
            .Select(group => new
            {
                LastName = group.Key,
                Users = group.OrderBy(user => user.DateOfBirth)
                    .Select(user => new
                    {
                        user.FirstName,
                        BirthDate = user.DateOfBirth.ToShortDateString()
                    })
            });

        Console.WriteLine("\nPossible Relatives:");
        foreach (var group in possibleRelatives)
        {
            Console.WriteLine($"Last Name: {group.LastName}");
            foreach (var user in group.Users)
            {
                Console.WriteLine($"- Name: {user.FirstName}, Birth Date: {user.BirthDate}");
            }
        }
    }

    static List<User> GenerateUsers()
    {
        // Generate a collection of user objects
        var users = new List<User>
        {
            new User { FirstName = "John", LastName = "Doe", Email = "john@example.com", DateOfBirth = new DateTime(1990, 5, 15), UserId = 1 },
            new User { FirstName = "Alice", LastName = "Smith", Email = "alice@gmail.com", DateOfBirth = new DateTime(2000, 8, 20), UserId = 2 },
            new User { FirstName = "Bob", LastName = "Johnson", Email = "bob@gmail.com", DateOfBirth = new DateTime(1985, 2, 10), UserId = 3 },
        };

        return users;
    }

    static int CalculateAge(DateTime dateOfBirth)
    {
        var today = DateTime.Today;
        var age = today.Year - dateOfBirth.Year;
        if (dateOfBirth > today.AddYears(-age))
            age--;

        return age;
    }
}