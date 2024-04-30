using LostAnimals.Context.Entities;
using Microsoft.AspNetCore.Identity;

namespace LostAnimals.Context.Seeder;

public class DemoHelper
{
    public IEnumerable<AnimalKind> GetAnimalKinds = new List<AnimalKind>()
    {
        new AnimalKind()
        {
            Uid = Guid.NewGuid(),
            //Id = 1,
            AnimalKindName = "Собака"
        },
        new AnimalKind()
        {
            Uid = Guid.NewGuid(),
            //Id = 2,
            AnimalKindName = "Кошка"
        }
    };

    public IEnumerable<NoteCategory> GetNotesCategories = new List<NoteCategory>()
    {
        new NoteCategory()
        {
            Uid = Guid.NewGuid(),
            //Id = 1,
            CategoryName = "Потери"
        },
        new NoteCategory()
        {
            Uid = Guid.NewGuid(),
            //Id = 2,
            CategoryName = "Находки"
        }
    };

    public List<Breed> ReadBreedsFromCsv(string filePath)
    {
        var breeds = new List<Breed>();

        using (var reader = new StreamReader(filePath))
        {
            int i = 1;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                var breed = new Breed
                {
                    Uid = Guid.NewGuid(),
                    //Id = i,
                    AnimalKindID = int.Parse(values[0]),
                    BreedName = values[1]
                };
                i++;

                breeds.Add(breed);
            }
        }

        return breeds;
    }

    public async Task<IEnumerable<User>> GetUsersAsync(UserManager<User> userManager)
    {
        var users = new List<User>();

        for (int i = 1; i <= 10; i++)
        {
            var user = new User
            {
                UserName = $"User{i}",
                Email = $"user{i}@example.com",
                EmailConfirmed = true,
                PhoneNumber = null,
                PhoneNumberConfirmed = false
            };

            var result = await userManager.CreateAsync(user, $"password{i}");

            if (result.Succeeded)
            {
                users.Add(user);
            }
        }

        return users;
    }

    public PhotoGallery GetPhotoGallery()
    {
        return new PhotoGallery
        {
            Uid = Guid.Parse("5552ceec-0574-4919-8488-e8aa47e483c0"),
            //Id = 1,
            PhotoStorages = new List<PhotoStorage>
            {
                new PhotoStorage
                {
                    Uid = Guid.Parse("d477bd28-ec96-465f-a54a-e95ed307c210"),
                    PhotoGalleryID = 1,
                    PhotoName = "images\\5552ceec-0574-4919-8488-e8aa47e483c0\\d477bd28-ec96-465f-a54a-e95ed307c210.jpg"
                },
                new PhotoStorage
                {
                    Uid = Guid.Parse("e7367fdf-90c5-45e4-be16-cd56cc9b0b2f"),
                    PhotoGalleryID = 1,
                    PhotoName = "images\\5552ceec-0574-4919-8488-e8aa47e483c0\\e7367fdf-90c5-45e4-be16-cd56cc9b0b2f.jpg"
                }
            }
        };
    }

    public IEnumerable<Note> GetNotes = new List<Note>()
    {
        new Note
        {
            Uid = Guid.NewGuid(),
            //Id = 1,
            UserID = 1,
            CategoryID = 1,
            Title = "Пропала собака породы лабрадор",
            AnimalName = "Бонни",
            BreedID = 212,
            Content = "Пропала собака породы лабрадор чёрного цвета. Возраст 3 года. Отличительные особенности: белый окрас на груди и лапах. Пропала 5 апреля в районе парка.",
            Latitude = 55.751244,
            Longtitude = 37.618423,
            LastSeenDate = DateTime.Parse("2023-04-05"),
            CreatedDate = DateTime.Now.AddDays(-1),
            IsActive = true,
            PhoneNumber = "+79991234567"
        },
        new Note
        {
            Uid = Guid.NewGuid(),
            //Id = 2,
            UserID = 1,
            CategoryID = 2,
            Title = "Найдена собака породы хаски",
            BreedID = 323,
            Content = "Найдена взрослая собака, предположительно хаски. Отличительные особенности: голубые глаза. Найдена сегодня в районе жилого комплекса.",
            Latitude = 55.751244,
            Longtitude = 35.618423,
            LastSeenDate = DateTime.Now,
            CreatedDate = DateTime.Now.AddDays(1),
            IsActive = true,
            PhoneNumber = "+79991234568"
        },
        new Note
        {
            Uid = Guid.NewGuid(),
            //Id = 3,
            UserID = 2,
            CategoryID = 1,
            Title = "Пропала кошка породы британская",
            AnimalName = "Мурзик",
            BreedID = 433,
            Content = "Пропала кошка породы британская. Возраст 1 год. Отличительные особенности: серый окрас, короткая шерсть. Пропала 1 апреля в районе дома.",
            Latitude = 56.751244,
            Longtitude = 36.618423,
            LastSeenDate = DateTime.Parse("2023-04-01"),
            CreatedDate = DateTime.Now.AddHours(1),
            IsActive = true,
            PhoneNumber = "+79991234569"
        },
        new Note
        {
            Uid = Guid.NewGuid(),
            //Id = 4,
            UserID = 3,
            CategoryID = 1,
            Title = "Пропала кошка породы сиамская",
            AnimalName = "Клеопатра",
            BreedID = 487,
            Content = "Пропала кошка породы сиамская. Возраст 3 года. Отличительные особенности: голубые глаза, длинная шерсть. Пропала 4 апреля в районе парка.",
            Latitude = 54.751244,
            Longtitude = 35.618423,
            LastSeenDate = DateTime.Parse("2023-04-04"),
            CreatedDate = DateTime.Now.AddHours(-1),
            IsActive = true,
            PhoneNumber = "+79991234570"
         },
        new Note
        {
            Uid = Guid.NewGuid(),
            //Id = 5,
            UserID = 4,
            CategoryID = 1,
            Title = "Пропала собака породы бульдог",
            AnimalName = "Бобик",
            BreedID = 27,
            Content = "Пропала собака породы бульдог. Возраст 4 года. Отличительные особенности: коричневый окрас, короткая шерсть. Пропала 2 апреля в районе магазина.",
            Latitude = 54.751244,
            Longtitude = 36.618423,
            LastSeenDate = DateTime.Parse("2023-04-02"),
            CreatedDate = DateTime.Now.AddHours(3),
            IsActive = true,
            PhoneNumber = "+79991234571"
        }
    };

    public IEnumerable<Comment> GetComments = new List<Comment>()
    {
         new Comment
         {
             Uid = Guid.NewGuid(),
             //Id = 1,
             UserID = 1,
             NoteID = 1,
             Content = "Я видел эту собаку вчера в парке. Она была с человеком в красной куртке.",
             CreatedDate = DateTime.Now,
         },
         new Comment
         {
             Uid = Guid.NewGuid(),
             //Id = 2,
             UserID = 2,
             NoteID = 1,
             Content = "Я тоже видел эту собаку. Она была без поводка и бежала по парку.",
             CreatedDate = DateTime.Now,
         },
         new Comment
         {
             Uid = Guid.NewGuid(),
             //Id = 3,
             UserID = 3,
             NoteID = 3,
             Content = "Я нашел эту кошку вчера вечером. Она была мокрой и испуганной. Я оставил ее у себя на ночь и сегодня привезу к ветеринару.",
             CreatedDate = DateTime.Now.AddDays(1),
         },
         new Comment
         {
             Uid = Guid.NewGuid(),
             //Id = 4,
             UserID = 4,
             NoteID = 2,
             Content = "Я видел эту собаку сегодня утром. Она была в автобусе и ехала одна.",
             CreatedDate = DateTime.Now.AddDays(1).AddHours(6),
         },
         new Comment
         {
             Uid = Guid.NewGuid(),
             //Id = 5,
             UserID = 5,
             NoteID = 2,
             ParentCommentID = 4,
             Content = "Вы уверены, что она была одна? Может быть, у нее был хозяин?",
             CreatedDate = DateTime.Now.AddHours(12),
         },
         new Comment
         {
             Uid = Guid.NewGuid(),
             //Id = 6,
             UserID = 6,
             NoteID = 4,
             Content = "Я нашел эту кошку вчера вечером. Она была мокрой и испуганной. Я оставил ее у себя на ночь и сегодня привезу к ветеринару.",
             CreatedDate = DateTime.Now.AddDays(1),
         },
         new Comment
         {
             Uid = Guid.NewGuid(),
             //Id = 7,
             UserID = 7,
             NoteID = 5,
             Content = "Я видел эту собаку вчера в парке. Она была с человеком в красной куртке.",
             CreatedDate = DateTime.Now.AddDays(1),
         },
         new Comment
         {
             Uid = Guid.NewGuid(),
             //Id = 8,
             UserID = 8,
             NoteID = 5,
             Content = "Я тоже видел эту собаку. Она была без поводка и бежала по парку.",
             CreatedDate = DateTime.Now.AddHours(14),
         },
         new Comment
         {
             Uid = Guid.NewGuid(),
             //Id = 9,
             UserID = 9,
             NoteID = 5,
             Content = "Я видел эту собаку вчера, но не успел поймать.",
             CreatedDate = DateTime.Now.AddDays(1),
         },
         new Comment
         {
             Uid = Guid.NewGuid(),
             //Id = 10,
             UserID = 10,
             NoteID = 5,
             Content = "Я видел похожую собаку вчера в парке, вроде бы с хозяином.",
             CreatedDate = DateTime.Now.AddDays(1)
         }
    };
}