using LostAnimals.Web.Pages.Breed.Models;
using System.Net.Http.Json;

namespace LostAnimals.Web.Pages.Breed.Services;

public class BreedService : IBreedService
{
    private IHttpClientFactory httpClientFactory;

    public BreedService(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<BreedViewModel>> GetBreeds()
    {
        var client = httpClientFactory.CreateClient("ApiClient");
        var response = await client.GetAsync("v1/breed");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<BreedViewModel>>() ?? new List<BreedViewModel>();
    }
}
