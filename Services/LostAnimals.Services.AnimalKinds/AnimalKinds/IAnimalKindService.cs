namespace LostAnimals.Services.AnimalKinds;

public interface IAnimalKindService
{
    Task<IEnumerable<AnimalKindModel>> GetAll();
    Task<AnimalKindModel> GetById(Guid id);
    Task<AnimalKindModel> Create(CreateAnimalKindModel model);
    Task Update(Guid id, UpdateAnimalKindModel model);
    Task Delete(Guid id);
}
