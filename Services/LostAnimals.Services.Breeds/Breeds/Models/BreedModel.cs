using AutoMapper;
using LostAnimals.Context;
using LostAnimals.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Services.Breeds;

public class BreedModel
{
    public Guid Id { get; set; }
    public string BreedName { get; set; }
    //public Guid AnimalKindID { get; set; }
    //public string AnimalKindName { get; set; }
}

public class BreedModelProfile : Profile
{
    public BreedModelProfile()
    {
        CreateMap<Breed, BreedModel>()
            .BeforeMap<BreedModelActions>()
            /*.ForMember(dest => dest.AnimalKindID, opt => opt.Ignore())
            .ForMember(dest => dest.AnimalKindName, opt => opt.Ignore())*/
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }

    public class BreedModelActions : IMappingAction<Breed, BreedModel>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public BreedModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(Breed source, BreedModel destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var breed = db.Breeds
                //.Include(x => x.AnimalKind)
                .FirstOrDefault(x => x.Id == source.Id);

            destination.Id = breed.Uid;
            /*destination.AnimalKindID = breed.AnimalKind.Uid;
            destination.AnimalKindName = breed.AnimalKind.AnimalKindName;*/
        }
    }
}