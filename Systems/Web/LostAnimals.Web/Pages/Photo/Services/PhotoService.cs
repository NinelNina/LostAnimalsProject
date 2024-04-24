using LostAnimals.Web.Pages.Photo.Models;
using System.Net.Http.Json;

namespace LostAnimals.Web.Pages.Photo.Services;

public class PhotoService : IPhotoService
{
    private readonly HttpClient httpClient;

    public PhotoService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task DeletePhoto(Guid photoId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<PhotoStorageViewModel>> GetPhotosByGalleryId(Guid galleryId)
    {
        var response = await httpClient.GetAsync($"v1/photo/{galleryId}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<PhotoStorageViewModel>>() ?? new List<PhotoStorageViewModel>();
    }
}

