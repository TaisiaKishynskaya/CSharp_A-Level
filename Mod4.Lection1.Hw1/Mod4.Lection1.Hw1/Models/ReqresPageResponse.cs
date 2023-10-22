using Newtonsoft.Json;

namespace Mod4.Lection1.Hw1.Models;


//Класс для десериализации JSON-ответа от веб-сервиса в объекты C#
internal class ReqresPageResponse
{
    public int Page { get; set; }

    // Свойство PerPage соответствует числовому полю per_page в JSON-ответе. То есть атрибут позволяет сопоставить JSON-поле с C#-свойством.
    [JsonProperty("per_page")] // чтобы указать, что имя поля в JSON-ответе отличается от имени свойства в классе. 
    public int PerPage { get; set; }

    public int Total { get; set; }

    [JsonProperty("total_pages")]
    public int TotalPages { get; set; }

    public ReqresUserRequest? DataForSingle { get; set; }
    public ICollection<ReqresUserRequest>? DataForList { get; set; } // ICollection используется для хранения списка элементов

    public override string ToString()
    {
        return $"Page: {Page} | PerPage: {PerPage} | Total: {Total} | TotalPages: {TotalPages}";
    }
}
