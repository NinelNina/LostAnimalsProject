namespace LostAnimals.Services.Breeds;

public interface IBreedService
{
    Task<IEnumerable<BreedModel>> GetAll();
    Task<BreedModel> GetById(Guid id);
    Task<BreedModel> Create(CreateBreedModel model);
    Task Update(Guid id, UpdateBreedModel model);
    Task Delete(Guid id);
}
