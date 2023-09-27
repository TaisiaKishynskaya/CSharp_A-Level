namespace Mod3.Lection2.Hw1._1;

internal class Program
{
    static void Main()
    {
        var order = new Post();

        order.MailArrived += Email.Send;
        order.MailArrived += SMS.Send;

        order.OnMailArrived();
    }
}
