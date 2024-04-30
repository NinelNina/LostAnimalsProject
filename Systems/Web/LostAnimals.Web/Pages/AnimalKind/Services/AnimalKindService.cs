using LostAnimals.Web.Pages.AnimalKind.Models;
using System.Net.Http.Json;

namespace LostAnimals.Web.Pages.AnimalKind.Services;

public class AnimalKindService(HttpClient httpClient) : IAnimalKindService
{
    public async Task<IEnumerable<AnimalKindViewModel>> GetAnimalKinds()
    {
        var response = await httpClient.GetAsync("v1/AnimalKind");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<AnimalKindViewModel>>() ?? new List<AnimalKindViewModel>();
    }
}
