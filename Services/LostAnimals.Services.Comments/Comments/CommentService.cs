using AutoMapper;
using LostAnimals.Common.Exceptions;
using LostAnimals.Context.Entities;
using LostAnimals.Context;
using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Services.Comments;

public class CommentService : ICommentService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;

    public CommentService(IDbContextFactory<MainDbContext> dbContextFactory, IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }

    public async Task<CommentModel> Create(CreateCommentModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var comment = mapper.Map<Comment>(model);

        await context.Comments.AddAsync(comment);

        await context.SaveChangesAsync();

        return mapper.Map<CommentModel>(comment);
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var comment = await context.Comments.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (comment == null)
        {
            throw new ProcessException($"Comment (ID = {id}) not found.");
        }

        context.Comments.Remove(comment);

        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<CommentModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var comments = await context.Comments
                .Include(x => x.User)
                .Include(x => x.Note)
                .Include(x => x.ParentComment)
                .Include(x => x.PhotoGallery)
                .ToListAsync();

        var result = mapper.Map<IEnumerable<CommentModel>>(comments);

        return result;
    }

    public async Task<CommentModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var comment = context.Comments
                .Include(x => x.User)
                .Include(x => x.Note)
                .Include(x => x.ParentComment)
                .Include(x => x.PhotoGallery)
                .FirstOrDefault(x => x.Uid == id);

        var result = mapper.Map<CommentModel>(comment);

        return result;
    }
}
