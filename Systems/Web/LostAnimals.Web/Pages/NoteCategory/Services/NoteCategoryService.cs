using System.Net.Http.Json;
using LostAnimals.Web.Pages.NoteCategory.Models;

namespace LostAnimals.Web.Pages.NoteCategory.Services;

public class NoteCategoryService : INoteCategoryService
{
    private IHttpClientFactory httpClientFactory;

    public NoteCategoryService(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<NoteCategoryViewModel>> GetCategories()
    {
        var client = httpClientFactory.CreateClient("ApiClient");
        var response = await client.GetAsync("v1/noteCategory");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<NoteCategoryViewModel>>() ?? new List<NoteCategoryViewModel>();
    }

    public async Task<NoteCategoryViewModel> GetNoteCategory(Guid id)
    {
        var client = httpClientFactory.CreateClient("ApiClient");
        var response = await client.GetAsync($"v1/noteCategory/{id}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<NoteCategoryViewModel>() ?? new();
    }
    
    public async Task<NoteCategoryViewModel> GetNoteCategoryByName(string name)
    {
        var client = httpClientFactory.CreateClient("ApiClient");
        var response = await client.GetAsync($"v1/noteCategory/{name}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<NoteCategoryViewModel>() ?? new();
    }
}
