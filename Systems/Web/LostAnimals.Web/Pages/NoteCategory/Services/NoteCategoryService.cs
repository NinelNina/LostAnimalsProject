using System.Net.Http.Json;
using LostAnimals.Web.Pages.NoteCategory.Models;

namespace LostAnimals.Web.Pages.NoteCategory.Services;

public class NoteCategoryService(HttpClient httpClient) : INoteCategoryService
{
    public async Task<IEnumerable<NoteCategoryViewModel>> GetCategories()
    {
        var response = await httpClient.GetAsync("v1/noteCategory");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<NoteCategoryViewModel>>() ?? new List<NoteCategoryViewModel>();
    }

    public async Task<NoteCategoryViewModel> GetNoteCategory(Guid id)
    {
        var response = await httpClient.GetAsync($"v1/noteCategory/{id}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<NoteCategoryViewModel>() ?? new();
    }
    
    public async Task<NoteCategoryViewModel> GetNoteCategoryByName(string name)
    {
        var response = await httpClient.GetAsync($"v1/noteCategory/{name}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<NoteCategoryViewModel>() ?? new();
    }
}
