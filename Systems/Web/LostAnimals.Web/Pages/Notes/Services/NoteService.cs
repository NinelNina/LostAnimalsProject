using System.Net.Http;
using System.Net.Http.Json;
using LostAnimals.Web.Pages.Notes.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace LostAnimals.Web.Pages.Notes.Services;

public class NoteService : INoteService
{
    private IHttpClientFactory httpClientFactory;

    public NoteService(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

    public async Task<PagedResult<NoteViewModel>> GetNotes(int page = 1, int pageSize = 10)
    {
        var client = httpClientFactory.CreateClient("ApiClient");
        var response = await client.GetAsync($"v1/note?page={page}&pageSize={pageSize}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
        return await response.Content.ReadFromJsonAsync<PagedResult<NoteViewModel>>() ?? new PagedResult<NoteViewModel>();
    }

    public async Task<NoteViewModel> GetNote(Guid id)
    {
        var client = httpClientFactory.CreateClient("ApiClient");
        var response = await client.GetAsync($"v1/note/{id}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<NoteViewModel>() ?? new();
    }

    public async Task<NoteViewModel> AddNote(CreateNoteViewModel model)
    {

        var requestContent = JsonContent.Create(model);
        var client = httpClientFactory.CreateClient("ApiClient");
        var response = await client.PostAsync("v1/note", requestContent);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<NoteViewModel>() ?? new();
    }

    public async Task EditNote(Guid noteId, UpdateNoteViewModel model)
    {
        var requestContent = JsonContent.Create(model);
        var client = httpClientFactory.CreateClient("ApiClient");
        var response = await client.PutAsync($"v1/note/{noteId}", requestContent);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task DeleteNote(Guid noteId)
    {
        var client = httpClientFactory.CreateClient("ApiClient");
        var response = await client.DeleteAsync($"v1/note/{noteId}");

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task<bool> UploadPhotoAsync(Guid noteId, IBrowserFile file)
    {
        if (file == null)
        {
            throw new ArgumentNullException(nameof(file));
        }

        var content = new MultipartFormDataContent();

        using var stream = file.OpenReadStream();
        var fileContent = new StreamContent(stream);
        content.Add(fileContent, "file", file.Name);

        var client = httpClientFactory.CreateClient("ApiClient");
        var response = await client.PostAsync($"/v1/Note/{noteId}/Photo/upload", content);

        return response.IsSuccessStatusCode;
    }
}
