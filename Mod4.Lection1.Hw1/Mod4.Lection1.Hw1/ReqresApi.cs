using Newtonsoft.Json;
using System.Net;
using System.Text;
using Newtonsoft.Json.Serialization;
using Mod4.Lection1.Hw1.Models;

namespace Mod4.Lection1.Hw1;

internal class ReqresApi
{
    private const string _reqresURL = "https://reqres.in/";
    private const string _apiRegister = "/api/register";
    private const string _apiLogin = "/api/login";

    private const string _notFoundMessage = $"\n404 - NotFound Code from reqres site.";
    private const string _badRequestMessage = $"\n400 - BadRequest Code from reqres site.";
    private const string _okMessage = $"\n200 - Ok Code from reqres site.";

    public static async Task ReadAndPrintFromJson(HttpResponseMessage result)
    {
        var content = await result.Content.ReadAsStringAsync(); // асинхронно считываем содержимое ответa
        Console.WriteLine(content);

        // библиотеку JsonConvert: десериализовать строку JSON (content) в объект типа ReqresPageResponse. JSON-строка преобразуется в экземпляр этого объекта.
        var pageRequest = JsonConvert.DeserializeObject<ReqresPageResponse>(content);

        if (pageRequest is not null)
        {
            Console.WriteLine(pageRequest.ToString());
        }
    }

    /*public static void SerializationOptions()
    {
        // для сериализации объекта userParametersRequest в строку JSON; Опции сериализации задаются в JsonSerializerSettings 
        var serializedUser = JsonConvert.SerializeObject(userParametersRequest, new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy() // параметр - стиль именования полей (в данном случае, CamelCase)
            }
        });
    }*/

    public static async Task GetListUsersAsync()
    {
        using var client = new HttpClient(); // т.к unmanaged (реализует IDispose)
        // установка базового адреса (все последующие HTTP-запросы, выполненные через клиент, будут относиться к этому базовому URL)
        client.BaseAddress = new Uri(_reqresURL);

        var result = await client.GetAsync("api/users?page=2"); // https://reqres.in/api/users?page=2

        if (result.StatusCode == HttpStatusCode.OK)
        {
            Console.WriteLine(_okMessage);

            await ReadAndPrintFromJson(result);
        }
    }

    public static async Task GetSingleUserAsync()
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(_reqresURL);

        var result = await client.GetAsync("/api/users/2");

        if (result.StatusCode == HttpStatusCode.OK)
        {
            Console.WriteLine(_okMessage);

            await ReadAndPrintFromJson(result);
        }
    }

    public static async Task GetSingleUserNotFound()
    {
        using var client = new HttpClient { BaseAddress = new Uri(_reqresURL)};

        var result = await client.GetAsync("/api/users/23");

        if (result.StatusCode == HttpStatusCode.NotFound)
        {
            Console.WriteLine(_notFoundMessage);

            await ReadAndPrintFromJson(result);
        }
    }

    public static async Task GetListRecourceAsync()
    {
        using var client = new HttpClient { BaseAddress = new Uri(_reqresURL) };

        var result = await client.GetAsync("/api/unknown");

        if (result.StatusCode == HttpStatusCode.OK)
        {
            Console.WriteLine(_okMessage);

            await ReadAndPrintFromJson(result);
        }
    }

    public static async Task GetSingleRecourceAsync()
    {
        using var client = new HttpClient { BaseAddress = new Uri(_reqresURL) };

        var result = await client.GetAsync("/api/unknown/2");

        if (result.StatusCode == HttpStatusCode.OK)
        {
            Console.WriteLine(_okMessage);

            await ReadAndPrintFromJson(result);
        }
    }

    public static async Task GetSingleRecourceNotFound()
    {
        using var client = new HttpClient { BaseAddress = new Uri(_reqresURL) };

        var result = await client.GetAsync("/api/unknown/23");

        if (result.StatusCode == HttpStatusCode.NotFound)
        {
            Console.WriteLine(_notFoundMessage);

            await ReadAndPrintFromJson(result);
        }
    }

    public static async Task GetDelayedResponseAsync()
    {
        using var client = new HttpClient { BaseAddress = new Uri(_reqresURL) };

        var result = await client.GetAsync("/api/users?delay=3");

        if (result.StatusCode == HttpStatusCode.OK)
        {
            Console.WriteLine(_okMessage);

            await ReadAndPrintFromJson(result);
        }
    }


    public static async Task PostCreateAsync()
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(_reqresURL);

        var userParametersRequest = new CreateUserParametersRequest
        {
            Name = "Taisiia",
            Job = "Student"
        };

        // для сериализации объекта userParametersRequest в строку JSON; Опции сериализации задаются в JsonSerializerSettings 
        var serializedUser = JsonConvert.SerializeObject(userParametersRequest, new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy() // параметр - стиль именования полей (в данном случае, CamelCase)
            }
        });

        // представляет данные, отправляемые в POST-запросе;  application/json - required (тип контента)
        var stringContent = new StringContent(serializedUser, Encoding.Unicode, "application/json"); 

        var result = await client.PostAsync("api/users", stringContent);

        if (result.StatusCode == HttpStatusCode.Created)
        {
            Console.WriteLine($"\nStatusCode Created");

            var content = await result.Content.ReadAsStringAsync();
            Console.WriteLine(content);

            var userCreatedResponse = JsonConvert.DeserializeObject<UserCreatedResponse>(content);
            Console.WriteLine(userCreatedResponse!.Name);
        }
    }

    public static async Task PostRegisterSuccessfulAsync()
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(_reqresURL);

        var userParametersRequest = new CreateUserParametersRequest
        {
            Email = "eve.holt@reqres.in",
            Password = "pistol"
        };

        var serializedUser = JsonConvert.SerializeObject(userParametersRequest, new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            }
        });

        var stringContent = new StringContent(serializedUser, Encoding.Unicode, "application/json");

        var result = await client.PostAsync(_apiRegister, stringContent);

        if (result.StatusCode == HttpStatusCode.OK)
        {
            Console.WriteLine(_okMessage);

            var content = await result.Content.ReadAsStringAsync();
            Console.WriteLine(content);

            var userCreatedResponse = JsonConvert.DeserializeObject<UserCreatedResponse>(content);
            Console.WriteLine(userCreatedResponse!.Name);
        }
    }

    public static async Task PostLoginSuccessfulAsync()
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(_reqresURL);

        var userParametersRequest = new CreateUserParametersRequest
        {
            Email = "eve.holt@reqres.in",
            Password = "cityslicka"
        };

        var serializedUser = JsonConvert.SerializeObject(userParametersRequest, new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            }
        });

        var stringContent = new StringContent(serializedUser, Encoding.Unicode, "application/json");

        var result = await client.PostAsync(_apiLogin, stringContent);

        if (result.StatusCode == HttpStatusCode.OK)
        {
            Console.WriteLine(_okMessage);

            var content = await result.Content.ReadAsStringAsync();
            Console.WriteLine(content);

            var userCreatedResponse = JsonConvert.DeserializeObject<UserCreatedResponse>(content);
            Console.WriteLine(userCreatedResponse!.Name);
        }
    }

    public static async Task PostRegisterUnsuccessfulAsync()
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(_reqresURL);

        var userParametersRequest = new CreateUserParametersRequest
        {
            Email = "sydney@fife"
        };

        var serializedUser = JsonConvert.SerializeObject(userParametersRequest, new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            }
        });

        var stringContent = new StringContent(serializedUser, Encoding.Unicode, "application/json");

        var result = await client.PostAsync(_apiRegister, stringContent);

        if (result.StatusCode == HttpStatusCode.BadRequest)
        {
            Console.WriteLine(_badRequestMessage);

            var content = await result.Content.ReadAsStringAsync();
            Console.WriteLine(content);

            var userCreatedResponse = JsonConvert.DeserializeObject<UserCreatedResponse>(content);
            Console.WriteLine(userCreatedResponse!.Name);
        }
    }

    public static async Task PostLoginUnsuccessfulAsync()
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(_reqresURL);

        var userParametersRequest = new CreateUserParametersRequest
        {
            Email = "peter@klaven"
        };

        var serializedUser = JsonConvert.SerializeObject(userParametersRequest, new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            }
        });

        var stringContent = new StringContent(serializedUser, Encoding.Unicode, "application/json");

        var result = await client.PostAsync(_apiLogin, stringContent);

        if (result.StatusCode == HttpStatusCode.BadRequest)
        {
            Console.WriteLine(_badRequestMessage);

            var content = await result.Content.ReadAsStringAsync();
            Console.WriteLine(content);

            var userCreatedResponse = JsonConvert.DeserializeObject<UserCreatedResponse>(content);
            Console.WriteLine(userCreatedResponse!.Name);
        }
    }
}
