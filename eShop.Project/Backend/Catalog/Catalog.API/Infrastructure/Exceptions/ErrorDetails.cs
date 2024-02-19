﻿namespace Catalog.API.Infrastructure.Exceptions;

// Цей код визначає клас ErrorDetails, який представляє об'єкт для зберігання деталей помилки.

public class ErrorDetails
{
    public int StatusCode { get; set; } // представляє код статусу HTTP відповіді. Вона використовується для передачі інформації про статус помилки.
    public string? Message { get; set; } // представляє повідомлення про помилку. Вона містить текстове повідомлення, яке пояснює причину помилки або надає додаткові відомості про помилку.
}

// Цей клас ErrorDetails може використовуватися для створення об'єктів, які містять деталі помилки, які потім можуть бути повернуті у відповіді API при виникненні помилки.
// Наприклад, цей клас може використовуватися для створення об'єкта, який містить код статусу помилки та повідомлення про помилку, які потім можуть бути повернуті у відповіді API, щоб інформувати клієнтів про виниклу помилку.
