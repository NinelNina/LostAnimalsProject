namespace LostAnimals.Services.NoteCategories;

public interface INoteCategoryService
{
    /// <summary>
    /// Get all note categories
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<NoteCategoryModel>> GetAll();

    /// <summary>
    /// Get note category by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<NoteCategoryModel> GetById(Guid id);
    
    /// <summary>
    /// Get note category by name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<NoteCategoryModel> GetByName(string name);

    /// <summary>
    /// Create note category
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<NoteCategoryModel> Create(CreateNoteCategoryModel model);

    /// <summary>
    /// Update note category
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    Task Update(Guid id, UpdateNoteCategoryModel model);

    /// <summary>
    /// Delete note category
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task Delete(Guid id);
}
