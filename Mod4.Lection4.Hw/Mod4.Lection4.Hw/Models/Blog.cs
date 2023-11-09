namespace Mod4.Lection4.Hw.Models;

public class Blog
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    //*-*
    public ICollection<User>? Readers { get; set; }

    //1-*
    public ICollection<Article>? Articles { get; set; }
}
