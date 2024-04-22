using System.Net.Http.Json;
using LostAnimals.Web.Pages.Notes.Models;

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

    public async Task<NoteViewModel> GetNote(Guid noteId)
    {
        var response = await httpClient.GetAsync($"v1/note/{noteId}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<NoteViewModel>() ?? new();
    }

    public async Task AddNote(CreateNoteViewModel model)
    {

        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PostAsync("v1/note", requestContent);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
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
}
