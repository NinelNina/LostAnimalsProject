using LostAnimals.Web.Pages.Breed.Models;
using System.Net.Http.Json;

namespace LostAnimals.Web.Pages.Breed.Services;

public class BreedService(HttpClient httpClient) : IBreedService
{
    public async Task<IEnumerable<BreedViewModel>> GetBreeds()
    {
        var response = await httpClient.GetAsync("v1/breed");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<BreedViewModel>>() ?? new List<BreedViewModel>();
    }
}
