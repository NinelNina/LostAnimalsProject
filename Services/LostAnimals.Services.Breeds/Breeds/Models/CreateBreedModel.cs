using AutoMapper;
using LostAnimals.Context;
using LostAnimals.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Services.Breeds;

public class CreateBreedModel
{
    public Guid AnimalKindID { get; set; }
    public string BreedName { get; set; }
}

public class CreateBreedModelProfile : Profile
{
    public CreateBreedModelProfile()
    {
        CreateMap<CreateBreedModel, Breed>()
            .ForMember(dest => dest.AnimalKindID, opt => opt.Ignore())
            .AfterMap<CreateBreedModelActions>();
    }
}

public class CreateBreedModelActions : IMappingAction<CreateBreedModel, Breed>
{
    private readonly IDbContextFactory<MainDbContext> contextFactory;

    public CreateBreedModelActions(IDbContextFactory<MainDbContext> contextFactory)
    {
        this.contextFactory = contextFactory;
    }

    public void Process(CreateBreedModel source, Breed destination, ResolutionContext context)
    {
        using var db = contextFactory.CreateDbContext();

        var animalKind = db.AnimalKinds.FirstOrDefault(x => x.Uid == source.AnimalKindID);

        destination.AnimalKindID = animalKind.Id;
    }
}