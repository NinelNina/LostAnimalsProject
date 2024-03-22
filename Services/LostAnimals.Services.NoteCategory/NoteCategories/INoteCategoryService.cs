namespace LostAnimals.Services.NoteCategories;

public interface INoteCategoryService
{
    Task<IEnumerable<NoteCategoryModel>> GetAll();
    Task<NoteCategoryModel> GetById(Guid id);
    Task<NoteCategoryModel> Create(CreateNoteCategoryModel model);
    Task Update(Guid id, UpdateNoteCategoryModel model);
    Task Delete(Guid id);
}
