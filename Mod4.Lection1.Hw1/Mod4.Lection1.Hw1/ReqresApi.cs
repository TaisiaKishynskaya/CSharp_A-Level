using Newtonsoft.Json;
using System.Net;
using System.Text;
using Newtonsoft.Json.Serialization;
using Mod4.Lection1.Hw1.Models;

namespace Mod4.Lection1.Hw1;

internal class ReqresApi
{
    private const string _reqresURL = "https://reqres.in/";
    private const string _apiUsers2 = "/api/users/2";
    private const string _apiRegister = "/api/register";
    private const string _apiLogin = "/api/login";

    private const string _contentType = "application/json";

    private const string _notFoundMessage = $"\n404 - NotFound Code from reqres site.";
    private const string _badRequestMessage = $"\n400 - BadRequest Code from reqres site.";
    private const string _okMessage = $"\n200 - Ok Code from reqres site.";


    private static async Task<string> ReadAndPrint(HttpResponseMessage request)
    {
        var content = await request.Content.ReadAsStringAsync();
        Console.WriteLine(content);

        return content;
    }

    public static async Task Deserialization(HttpResponseMessage result)
    {
        var content = await ReadAndPrint(result); // асинхронно считываем содержимое ответa

        // библиотеку JsonConvert: десериализовать строку JSON (content) в объект типа ReqresPageResponse. JSON-строка преобразуется в экземпляр этого объекта.
        var pageRequest = JsonConvert.DeserializeObject<ReqresPageResponse>(content);

        if (pageRequest is not null)
        {
            Console.WriteLine(pageRequest.ToString());
        }
    }

    private static HttpContent Serealization(CreateUserParametersRequest userParametersRequest)
    {
        // для сериализации объекта userParametersRequest в строку JSON; Опции сериализации задаются в JsonSerializerSettings 
        var serializedUser = JsonConvert.SerializeObject(userParametersRequest, new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy() // параметр - стиль именования полей (в данном случае, CamelCase)
            }
        });

        // представляет данные, отправляемые в POST-запросе;  application/json - required (тип контента)
        var stringContent = new StringContent(serializedUser, Encoding.Unicode, _contentType);
        return stringContent;
    }


    public static async Task GetListUsersAsync()
    {
        using var client = new HttpClient(); // т.к unmanaged (реализует IDispose)
        // установка базового адреса (все последующие HTTP-запросы, выполненные через клиент, будут относиться к этому базовому URL)
        client.BaseAddress = new Uri(_reqresURL);

        var response = await client.GetAsync("api/users?page=2"); // https://reqres.in/api/users?page=2

        if (response.StatusCode == HttpStatusCode.OK)
        {
            Console.WriteLine(_okMessage);

            await Deserialization(response);
        }
    }

    public static async Task GetSingleUserAsync()
    {
        using var client = new HttpClient { BaseAddress = new Uri(_reqresURL) };

        var response = await client.GetAsync(_apiUsers2);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            Console.WriteLine(_okMessage);

            await ReadAndPrint(response);
        }
    }

    public static async Task GetSingleUserNotFound()
    {
        using var client = new HttpClient { BaseAddress = new Uri(_reqresURL)};

        var response = await client.GetAsync("/api/users/23");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            Console.WriteLine(_notFoundMessage);

            await ReadAndPrint(response);
        }
    }

    public static async Task GetListRecourceAsync()
    {
        using var client = new HttpClient { BaseAddress = new Uri(_reqresURL) };

        var response = await client.GetAsync("/api/unknown");

        if (response.StatusCode == HttpStatusCode.OK)
        {
            Console.WriteLine(_okMessage);

            await Deserialization(response);
        }
    }

    public static async Task GetSingleRecourceAsync()
    {
        using var client = new HttpClient { BaseAddress = new Uri(_reqresURL) };

        var response = await client.GetAsync("/api/unknown/2");

        if (response.StatusCode == HttpStatusCode.OK)
        {
            Console.WriteLine(_okMessage);

            await ReadAndPrint(response);
        }
    }

    public static async Task GetSingleRecourceNotFound()
    {
        using var client = new HttpClient { BaseAddress = new Uri(_reqresURL) };

        var response = await client.GetAsync("/api/unknown/23");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            Console.WriteLine(_notFoundMessage);

            await ReadAndPrint(response);
        }
    }

    public static async Task GetDelayedResponseAsync()
    {
        using var client = new HttpClient { BaseAddress = new Uri(_reqresURL) };

        var response = await client.GetAsync("/api/users?delay=3");

        if (response.StatusCode == HttpStatusCode.OK)
        {
            Console.WriteLine(_okMessage);

            await Deserialization(response);
        }
    }


    public static async Task PostCreateAsync()
    {
        using var client = new HttpClient { BaseAddress = new Uri(_reqresURL) };

        var userParametersRequest = new CreateUserParametersRequest
        {
            Name = "taisiia",
            Job = "student"
        };

        var stringContent = Serealization(userParametersRequest);

        var response = await client.PostAsync("api/users", stringContent);

        if (response.StatusCode == HttpStatusCode.Created)
        {
            Console.WriteLine($"\n201 - Created Code from reqres site.");

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);

            var userCreatedResponse = JsonConvert.DeserializeObject<UserCreatedResponse>(content);
            Console.WriteLine(userCreatedResponse!.Name);
        }
    }

    public static async Task PostRegisterSuccessfulAsync()
    {
        using var client = new HttpClient { BaseAddress = new Uri(_reqresURL) };

        var userParametersRequest = new CreateUserParametersRequest
        {
            Email = "eve.holt@reqres.in",
            Password = "pistol"
        };

        var stringContent = Serealization(userParametersRequest);

        var response = await client.PostAsync(_apiRegister, stringContent);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            Console.WriteLine(_okMessage);

            await ReadAndPrint(response);
        }
    }

    public static async Task PostLoginSuccessfulAsync()
    {
        using var client = new HttpClient { BaseAddress = new Uri(_reqresURL) };

        var userParametersRequest = new CreateUserParametersRequest
        {
            Email = "eve.holt@reqres.in",
            Password = "cityslicka"
        };

        var stringContent = Serealization(userParametersRequest);

        var response = await client.PostAsync(_apiLogin, stringContent);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            Console.WriteLine(_okMessage);

            await ReadAndPrint(response);
        }
    }

    public static async Task PostRegisterUnsuccessfulAsync()
    {
        using var client = new HttpClient { BaseAddress = new Uri(_reqresURL) };

        var userParametersRequest = new CreateUserParametersRequest
        {
            Email = "sydney@fife"
        };

        var stringContent = Serealization(userParametersRequest);

        var response = await client.PostAsync(_apiRegister, stringContent);

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            Console.WriteLine(_badRequestMessage);

            await ReadAndPrint(response);
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode}");
        }
    }

    public static async Task PostLoginUnsuccessfulAsync()
    {
        using var client = new HttpClient { BaseAddress = new Uri(_reqresURL) };

        var userParametersRequest = new CreateUserParametersRequest
        {
            Email = "peter@klaven"
        };

        var stringContent = Serealization(userParametersRequest);

        var response = await client.PostAsync(_apiLogin, stringContent);

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            Console.WriteLine(_badRequestMessage);

            await ReadAndPrint(response);
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode}");
        }
    }


    public static async Task PutUpdateAsync()
    {
        using var client = new HttpClient { BaseAddress = new Uri(_reqresURL) };

        var updatedData = new CreateUserParametersRequest // Заменить на данные, которые хотим отправить
        {
            Name = "morpheus",
            Job = "zion resident"
        };

        var stringContent = Serealization(updatedData);

        var response = await client.PutAsync(_apiUsers2, stringContent);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            Console.WriteLine($"{_okMessage}. Resource updated successfully.");

            await ReadAndPrint(response);
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode}");
        }
    }

    public static async Task PatchUpdateAsync()
    {
        using var client = new HttpClient { BaseAddress = new Uri(_reqresURL) };

        var updatedData = new CreateUserParametersRequest // Заменить на данные, которые хотим отправить
        {
            Name = "morpheus",
            Job = "zion resident"
        };

        var stringContent = Serealization(updatedData);

        var response = await client.PatchAsync(_apiUsers2, stringContent);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            Console.WriteLine($"{_okMessage}. Resource updated successfully.");

            await ReadAndPrint(response);
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode}");
        }
    }

    public static async Task DeleteInfoAsync()
    {
        using var client = new HttpClient { BaseAddress = new Uri(_reqresURL) };

        var response = await client.DeleteAsync(_apiUsers2);

        if (response.StatusCode == HttpStatusCode.NoContent)
        {
            Console.WriteLine($"\n204 - NoContent Code from reqres site.");

            await ReadAndPrint(response);
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode}");
        }
    }
}
