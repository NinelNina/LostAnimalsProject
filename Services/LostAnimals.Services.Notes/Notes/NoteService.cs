using AutoMapper;
using LostAnimals.Common.Exceptions;
using LostAnimals.Context;
using LostAnimals.Context.Entities;
using LostAnimals.Services.PhotoService;
using LostAnimals.Services.RabbitMqService;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace LostAnimals.Services.Notes;

public class NoteService : INoteService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IPhotoService photoService;
    private readonly IMessageQueueService rabbitMqService;

    public NoteService(
        IDbContextFactory<MainDbContext> dbContextFactory,
        IMapper mapper,
        IPhotoService photoService,
        IMessageQueueService rabbitMqService)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.photoService = photoService;
        this.rabbitMqService = rabbitMqService;
    }

    public async Task<NoteModel> Create(CreateNoteModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var note = mapper.Map<Note>(model);

        await context.Notes.AddAsync(note);

        await context.SaveChangesAsync();

        return mapper.Map<NoteModel>(note);
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var note = await context.Notes.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (note == null)
        {
            throw new ProcessException($"Note (ID = {id}) not found.");
        }

        context.Notes.Remove(note);

        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<NoteModel>> GetAll(int page = 1, int pageSize = 10)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var notes = await context.Notes
            .Include(x => x.User)
            .Include(x => x.Category)
            .Include(x => x.Breed)
            .ThenInclude(x => x.AnimalKind)
            .Include(x => x.Comments)
            .Include(x => x.PhotoGallery)
            .ThenInclude(x => x.PhotoStorages)
            .OrderByDescending(x => x.CreatedDate)  // Сортировка по дате создания
            .Skip((page - 1) * pageSize)           // Пропуск записей для предыдущих страниц
            .Take(pageSize)                        // Выбор нужного количества записей
            .ToListAsync();

        var result = mapper.Map<IEnumerable<NoteModel>>(notes);

        return result;
    }

    public async Task<int> GetTotalCount()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        return await context.Notes.CountAsync();
    }

    public async Task<NoteModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var note = context.Notes
                .Include(x => x.User)
                .Include(x => x.Category)
                .Include(x => x.Breed)
                .ThenInclude(x => x.AnimalKind)
                .Include(x => x.Comments)
                .Include(x => x.PhotoGallery)
                .ThenInclude(x => x.PhotoStorages)
                .FirstOrDefault(x => x.Uid == id);

        var result = mapper.Map<NoteModel>(note);

        return result;
    }

    public async Task Update(Guid id, UpdateNoteModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var note = await context.Notes
            .Include(x => x.User)
            .Include(x => x.Category)
            .Include(x => x.Breed)
            .ThenInclude(x => x.AnimalKind)
            .Include(x => x.Comments)
            .Include(x => x.PhotoGallery)
            .ThenInclude(x => x.PhotoStorages)
            .Where(x => x.Uid == id).FirstOrDefaultAsync();

        note = mapper.Map(model, note);

        context.Notes.Update(note);

        await context.SaveChangesAsync();
    }

    public async Task UploadPhoto(Guid id, IFormFile file)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var note = await context.Notes
            .Include(x => x.User)
            .Include(x => x.Category)
            .Include(x => x.Breed)
            .ThenInclude(x => x.AnimalKind)
            .Include(x => x.Comments)
            .Include(x => x.PhotoGallery)
            .ThenInclude(x => x.PhotoStorages)
            .Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (note == null)
        {
            throw new ProcessException($"Note (ID = {id}) not found.");
        }

        var model = mapper.Map<UpdateNoteModel>(note);
        var photoGalleryID = await photoService.UploadPhoto(file, model.PhotoGalleryId);

        if (photoGalleryID != null)
        {
            if (model.PhotoGalleryId == null)
            {
                model.PhotoGalleryId = photoGalleryID;
            }

            note = mapper.Map(model, note);
            context.Notes.Update(note);
            await context.SaveChangesAsync();

            // Отправка сообщения в RabbitMQ
            var photoMessage = new PhotoProcessingMessage
            {
                NoteId = note.Uid,
                PhotoId = note.PhotoGallery.PhotoStorages.Last().Uid, // Предполагается, что последняя добавленная фотография
                ImagePath = note.PhotoGallery.PhotoStorages.Last().PhotoName, // Путь к файлу из PhotoStorage
                AnimalType = note.Breed.AnimalKind.AnimalKindName // Тип животного
            };
            await rabbitMqService.PushAsync("animal_photos", photoMessage);
        }
    }
}

