using Microsoft.AspNetCore.Http;

namespace LostAnimals.Services.Notes;

public interface INoteService
{
    /// <summary>
    /// Get all notes
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<NoteModel>> GetAll();
    
    /// <summary>
    /// Get note by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<NoteModel> GetById(Guid id);
    
    /// <summary>
    /// Create note
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<NoteModel> Create(CreateNoteModel model);
    
    /// <summary>
    /// Update note
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    Task Update(Guid id, UpdateNoteModel model);
    
    /// <summary>
    /// Delete note
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task Delete(Guid id);

    /// <summary>
    /// Upload photo to note
    /// </summary>
    /// <param name="id"></param>
    /// <param name="file"></param>
    /// <returns></returns>
    Task UploadPhoto(Guid id, IFormFile file);
}
