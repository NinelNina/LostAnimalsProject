using AutoMapper;
using LostAnimals.Context;
using LostAnimals.Context.Entities;
using LostAnimals.Services.Notes;
using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Services.NoteCategories;

public class NoteCategoryModel
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; }
    public ICollection<NoteModel>? Notes { get; set; }
}

public class NoteCategoryModelProfile : Profile
{
    public NoteCategoryModelProfile()
    {
        CreateMap<NoteCategory, NoteCategoryModel>()
            .BeforeMap<NoteCategoryModelActions>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }

    public class NoteCategoryModelActions : IMappingAction<NoteCategory, NoteCategoryModel>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public NoteCategoryModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(NoteCategory source, NoteCategoryModel destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var category = db.NoteCategories
                .FirstOrDefault(x => x.Id == source.Id);

            destination.Id = category.Uid;
        }
    }
}