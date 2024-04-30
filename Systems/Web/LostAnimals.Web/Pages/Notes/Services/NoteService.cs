using System.Net.Http;
using System.Net.Http.Json;
using LostAnimals.Web.Pages.Notes.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace LostAnimals.Web.Pages.Notes.Services;

public class NoteService(HttpClient httpClient) : INoteService
{
    public async Task<IEnumerable<NoteViewModel>> GetNotes()
    {
        var response = await httpClient.GetAsync("v1/note");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<NoteViewModel>>() ?? new List<NoteViewModel>();
    }

    public async Task<NoteViewModel> GetNote(Guid id)
    {
        var response = await httpClient.GetAsync($"v1/note/{id}");
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
        var response = await httpClient.PostAsync("v1/note", requestContent);

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
        var response = await httpClient.PutAsync($"v1/note/{noteId}", requestContent);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task DeleteNote(Guid noteId)
    {
        var response = await httpClient.DeleteAsync($"v1/note/{noteId}");

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

        var response = await httpClient.PostAsync($"/v1/Note/{noteId}/Photo/upload", content);

        return response.IsSuccessStatusCode;
    }
}
