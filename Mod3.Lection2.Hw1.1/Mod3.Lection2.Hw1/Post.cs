namespace Mod3.Lection2.Hw1._1;

public class Post
{
    public event EventHandler? MailArrived;

    public void OnMailArrived()
    {
        Console.WriteLine("Letter already in post ofice!");

        MailArrived?.Invoke(this, EventArgs.Empty);
    }
}

public class Email
{
    public static void Send(object sender, EventArgs e)
    {
        Console.WriteLine($"Send an email: you have got a letter.");
    }
}

public class SMS
{
    public static void Send(object sender, EventArgs e)
    {
        Console.WriteLine($"Send an SMS: you have got a letter.");
    }
}