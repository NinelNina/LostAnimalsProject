using LostAnimals.Web.Pages.AnimalKind.Models;
using System.Net.Http.Json;

namespace LostAnimals.Web.Pages.AnimalKind.Services;

public class AnimalKindService : IAnimalKindService
{
    private readonly IHttpClientFactory httpClientFactory;

    public AnimalKindService(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<AnimalKindViewModel>> GetAnimalKinds()
    {
        var client = httpClientFactory.CreateClient("ApiClient");
        var response = await client.GetAsync("v1/AnimalKind");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<AnimalKindViewModel>>() ?? new List<AnimalKindViewModel>();
    }
}
