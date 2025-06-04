using LostAnimals.Web.Pages.Photo.Models;
using System.Net.Http.Json;

namespace LostAnimals.Web.Pages.Photo.Services;

public class PhotoService : IPhotoService
{
    private readonly IHttpClientFactory httpClientFactory;

    public PhotoService(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

    public async Task DeletePhoto(Guid photoId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<PhotoStorageViewModel>> GetPhotosByGalleryId(Guid galleryId)
    {
        var client = httpClientFactory.CreateClient("ApiClient");
        var response = await client.GetAsync($"v1/photo/{galleryId}");

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<PhotoStorageViewModel>>() ?? new List<PhotoStorageViewModel>();
    }
}

