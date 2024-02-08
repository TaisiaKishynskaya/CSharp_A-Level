using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Drawing;
using WebApp.Models;
using WebApp.Services.Abstractions;

namespace WebApp.Services;

public class CatalogService : ICatalogService
{
    private readonly IHttpClientFactory _clientFactory;

    public CatalogService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<PaginatedResponse<CatalogItemModel>> GetCatalogItems(int page, int size, string sort, List<int> types = null, List<int> brands = null)
    {
        var httpClient = _clientFactory.CreateClient();
        var accessToken = await GetAccessToken();
        httpClient.SetBearerToken(accessToken);

        var response = await httpClient.GetAsync($"http://localhost:5002/bff/catalog/items?page={page}&size={size}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PaginatedResponse<CatalogItemModel>>(content);

            if (types != null && types.Any())
            {
                result.Data = result.Data.Where(item => types.Contains(item.Type.Id)).ToList();
            }
            if (brands != null && brands.Any())
            {
                result.Data = result.Data.Where(item => brands.Contains(item.Brand.Id)).ToList();
            }


            switch (sort)
            {
                case "price_asc":
                    result.Data = result.Data.OrderBy(item => item.Price).ToList();
                    break;
                case "price_desc":
                    result.Data = result.Data.OrderByDescending(item => item.Price).ToList();
                    break;
                case "name_asc":
                    result.Data = result.Data.OrderBy(item => item.Title).ToList();
                    break;
                case "name_desc":
                    result.Data = result.Data.OrderByDescending(item => item.Title).ToList();
                    break;
                case "date_asc":
                    result.Data = result.Data.OrderBy(item => item.CreatedAt).ToList();
                    break;
                case "date_desc":
                    result.Data = result.Data.OrderByDescending(item => item.CreatedAt).ToList();
                    break;
            }

            return result;
        }
        else
        {
            throw new Exception("API request error");
        }
    }

    public async Task<PaginatedResponse<CatalogTypeModel>> GetCatalogTypes()
    {
        var httpClient = _clientFactory.CreateClient();
        var accessToken = await GetAccessToken();
        httpClient.SetBearerToken(accessToken);

        var response = await httpClient.GetAsync("http://localhost:5002/bff/catalog/types");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PaginatedResponse<CatalogTypeModel>>(content);
            return result;
        }
        else
        {
            throw new Exception("API request error");
        }
    }

    public async Task<PaginatedResponse<CatalogBrandModel>> GetCatalogBrands()
    {
        var httpClient = _clientFactory.CreateClient();
        var accessToken = await GetAccessToken();
        httpClient.SetBearerToken(accessToken);

        var response = await httpClient.GetAsync("http://localhost:5002/bff/catalog/brands");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PaginatedResponse<CatalogBrandModel>>(content);
            return result;
        }
        else
        {
            throw new Exception("API request error");
        }
    }

    public async Task<CatalogItemModel> GetCatalogItemById(int id)
    {
        var httpClient = _clientFactory.CreateClient();
        var accessToken = await GetAccessToken();
        httpClient.SetBearerToken(accessToken);

        var response = await httpClient.GetAsync($"http://localhost:5002/bff/catalog/items/{id}");
        if (response.IsSuccessStatusCode) 
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CatalogItemModel>(content);
            return result;
        }
        else
        {
            throw new Exception("API request error");
        }
    }


    private async Task<string> GetAccessToken()
    {
        var httpClient = _clientFactory.CreateClient();

        var disco = await httpClient.GetDiscoveryDocumentAsync("https://localhost:5001");
        if (disco.IsError)
        {
            throw new Exception("Discovery document error");
        }

        var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "mvc_client",
            ClientSecret = "mvc_secret",
            Scope = "WebBffAPI"
        });

        if (tokenResponse.IsError)
        {
            throw new Exception("Token request error");
        }

        return tokenResponse.AccessToken;
    }

}
