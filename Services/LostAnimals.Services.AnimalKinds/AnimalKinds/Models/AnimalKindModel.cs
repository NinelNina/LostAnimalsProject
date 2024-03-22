using AutoMapper;
using LostAnimals.Context;
using LostAnimals.Context.Entities;
using LostAnimals.Services.Breeds;
using Microsoft.EntityFrameworkCore;


namespace LostAnimals.Services.AnimalKinds;

public class AnimalKindModel
{
    public Guid Id { get; set; }

    public string AnimalKindName { get; set; }

    public ICollection<BreedModel>? Breeds { get; set; }
}

public class AnimalKindModelProfile : Profile
{
    public AnimalKindModelProfile()
    {
        CreateMap<AnimalKind, AnimalKindModel>()
            .BeforeMap<AnimalKindModelActions>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }

    public class AnimalKindModelActions : IMappingAction<AnimalKind, AnimalKindModel>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public AnimalKindModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(AnimalKind source, AnimalKindModel destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var animalKind = db.AnimalKinds
                .FirstOrDefault(x => x.Id == source.Id);

            destination.Id = animalKind.Uid;
        }
    }
}
