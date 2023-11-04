namespace Mod4.Lection4.Hw.Models;


public enum ThemeSetting
{
    Light, Dark, System
}

public class UserSettings
{
    public Guid Id { get; set; }
    public ThemeSetting Theme { get; set; }

    //1-1
    public int UserId { get; set; }
    public User? User { get; set; }
}
