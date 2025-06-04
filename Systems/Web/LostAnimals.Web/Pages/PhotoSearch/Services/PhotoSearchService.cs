using LostAnimals.Web.Pages.PhotoSearch.Models;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http;
using System.Net.Http.Json;

namespace LostAnimals.Web.Pages.PhotoSearch.Services;

public class PhotoSearchService : IPhotoSearchService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public PhotoSearchService(IHttpClientFactory clientFactory)
    {
        _httpClientFactory = clientFactory;
    }

    public async Task<List<SearchResult>> SearchByPhotoAsync(IBrowserFile file)
    {
        if (file == null)
        {
            throw new ArgumentNullException(nameof(file));
        }

        var content = new MultipartFormDataContent();
        using var stream = file.OpenReadStream();
        var fileContent = new StreamContent(stream);
        content.Add(fileContent, "file", file.Name);

        var client = _httpClientFactory.CreateClient("PhotoSearchClient");

        var response = await client.PostAsync("search", content);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception(errorContent);
        }

        var searchResponse = await response.Content.ReadFromJsonAsync<Dictionary<string, List<SearchResult>>>();
        
        return searchResponse?["results"] ?? new List<SearchResult>();
    }
}