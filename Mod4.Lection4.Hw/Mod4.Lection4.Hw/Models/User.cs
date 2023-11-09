using System.Reflection.Metadata;

namespace Mod4.Lection4.Hw.Models;

public class User // назва класу - назва таблиці
{
    // назва св-ва - назва колонки

    public int Id { get; set; }  // всі св-ва, що мають в назві id, за конвенцією будуть використані як первинний та вторинний ключі
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public DateOnly? Birthday { get; set; }  // DateOnly не існіє в скл, тому так воно не запрацює, двитись в конвенціях

    // 1-1
    public UserSettings? UserSettings { get; set; }

    // *-*
    public ICollection<Blog> BlogSubscribsions { get; set; } = new List<Blog>();

    // *-*
    public ICollection<Article> Articles { get; set; } = new List<Article>();
}
