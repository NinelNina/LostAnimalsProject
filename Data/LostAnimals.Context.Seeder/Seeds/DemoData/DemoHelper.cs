using LostAnimals.Context.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LostAnimals.Context.Seeder
{
    public class DemoHelper
    {
        // Виды животных
        public IEnumerable<AnimalKind> GetAnimalKinds => new List<AnimalKind>()
        {
            new AnimalKind { Uid = Guid.NewGuid(), AnimalKindName = "Собака" },
            new AnimalKind { Uid = Guid.NewGuid(), AnimalKindName = "Кошка" }
        };

        // Категории объявлений
        public IEnumerable<NoteCategory> GetNotesCategories => new List<NoteCategory>()
        {
            new NoteCategory { Uid = Guid.NewGuid(), CategoryName = "Потери" },
            new NoteCategory { Uid = Guid.NewGuid(), CategoryName = "Находки" }
        };

        // Породы животных из Breeds.csv
        public List<Breed> GetBreeds()
        {
            var dogKind = GetAnimalKinds.First(k => k.AnimalKindName == "Собака");
            var catKind = GetAnimalKinds.First(k => k.AnimalKindName == "Кошка");
            var breeds = new List<Breed>();

            // Чтение файла Breeds.csv
            var breedLines = File.ReadAllLines("Breeds.csv").Skip(1); // Пропускаем заголовок
            foreach (var line in breedLines)
            {
                var parts = line.Split(',');
                if (parts.Length >= 2)
                {
                    int animalKind = int.Parse(parts[0]);
                    string breedName = parts[1].Trim();
                    var kind = animalKind == 1 ? dogKind : catKind;
                    breeds.Add(new Breed
                    {
                        Uid = Guid.NewGuid(),
                        AnimalKind = kind,
                        BreedName = breedName
                    });
                }
            }

            return breeds;
        }

        // Создание пользователей
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
                };
                var result = await userManager.CreateAsync(user, $"password{i}");
                if (result.Succeeded)
                {
                    users.Add(user);
                }
            }
            return users;
        }

        // Список городов России с координатами
        private List<(string City, double Latitude, double Longitude)> GetRussianCities()
        {
            return new List<(string, double, double)>
            {
                ("Санкт-Петербург", 59.9342802, 30.3350986),
                ("Новосибирск", 55.0083526, 82.9357327),
                ("Екатеринбург", 56.8389261, 60.6057025),
                ("Казань", 55.8304307, 49.0660806),
                ("Нижний Новгород", 56.2965039, 43.936059),
                ("Челябинск", 55.1644419, 61.4368432),
                ("Самара", 53.2415041, 50.2212463),
                ("Омск", 54.9920442, 73.3242361),
                ("Ростов-на-Дону", 47.2357137, 39.701505),
                ("Уфа", 54.7387621, 55.9720554),
                ("Красноярск", 56.01839, 92.8671656),
                ("Воронеж", 51.6607812, 39.2002695),
                ("Пермь", 58.0103211, 56.2294436),
                ("Волгоград", 48.708048, 44.5133035),
                ("Краснодар", 45.0392674, 38.987221)
            };
        }

        // Получение списка фотографий из папки датасета
        private List<string> GetPhotosForAnimalKind(string animalKind)
        {
            string folder = animalKind == "Собака" ? "dogs" : "cats";
            string datasetPath = Path.Combine("/app/images/dataset", folder);
            string pattern = animalKind == "Собака" ? "dog.*.jpg" : "cat.*.jpg";

            if (!Directory.Exists(datasetPath))
            {
                Console.WriteLine($"Папка {datasetPath} не найдена.");
                return new List<string>();
            }

            try
            {
                return Directory
                    .GetFiles(datasetPath, pattern, SearchOption.TopDirectoryOnly)
                    .Select(Path.GetFileName)
                    .OrderBy(x => Guid.NewGuid()) // Случайная сортировка
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении файлов из {datasetPath}: {ex.Message}");
                return new List<string>();
            }
        }

        // Создание объявлений
        public IEnumerable<Note> GetNotes(IEnumerable<User> users, IEnumerable<NoteCategory> categories, IEnumerable<Breed> breeds)
        {
            var notes = new List<Note>();
            var random = new Random();
            var cities = GetRussianCities();

            // Ограничим до 30 случайных пород
            var selectedBreeds = breeds.OrderBy(x => random.Next()).Take(30).ToList();

            for (int i = 0; i < 30; i++)
            {
                var user = users.ElementAt(random.Next(users.Count()));
                var category = categories.ElementAt(random.Next(categories.Count()));
                var breed = selectedBreeds[i];
                var city = cities[random.Next(cities.Count)];
                string animalKindName = breed.AnimalKind.AnimalKindName;

                // Получаем список фотографий для данного вида животного
                var availablePhotos = GetPhotosForAnimalKind(animalKindName);
                var photoCount = availablePhotos.Any() ? random.Next(1, 3) : 0; // 1-2 фото, если есть
                var selectedPhotos = availablePhotos.Take(photoCount).ToList(); // Выбираем первые 1-2 после случайной сортировки

                var note = new Note
                {
                    Uid = Guid.NewGuid(),
                    User = user,
                    Category = category,
                    Title = category.CategoryName == "Потери"
                        ? $"Пропал(а) {animalKindName} породы {breed.BreedName}"
                        : $"Найден(а) {animalKindName} породы {breed.BreedName}",
                    AnimalName = category.CategoryName == "Потери" ? $"Питомец{i + 1}" : null,
                    Breed = breed,
                    Content = category.CategoryName == "Потери"
                        ? $"Пропал(а) {animalKindName} породы {breed.BreedName}. Возраст {random.Next(1, 10)} лет. Особые приметы: {GetRandomFeature()}. Пропал(а) {random.Next(1, 30)} апреля в районе {city.City}."
                        : $"Найден(а) {animalKindName} породы {breed.BreedName}. Возраст примерно {random.Next(1, 10)} лет. Особые приметы: {GetRandomFeature()}. Найден(а) {random.Next(1, 30)} апреля в районе {city.City}.",
                    Latitude = city.Latitude + random.NextDouble() * 0.01,
                    Longtitude = city.Longitude + random.NextDouble() * 0.01,
                    LastSeenDate = DateTime.Now.AddDays(-random.Next(1, 30)),
                    CreatedDate = DateTime.Now.AddDays(-random.Next(1, 30)),
                    IsActive = true,
                    PhoneNumber = $"+7999{random.Next(1000000, 9999999)}",
                    PhotoGallery = new PhotoGallery
                    {
                        Uid = Guid.NewGuid(),
                        PhotoStorages = selectedPhotos.Select(photo => new PhotoStorage
                        {
                            Uid = Guid.NewGuid(),
                            PhotoName = Path.Combine("images", "dataset", animalKindName == "Собака" ? "dogs" : "cats", photo).Replace("\\", "/")
                        }).ToList()
                    }
                };

                notes.Add(note);
            }

            return notes;
        }

        // Создание комментариев
        public IEnumerable<Comment> GetComments(IEnumerable<User> users, IEnumerable<Note> notes)
        {
            var comments = new List<Comment>();
            var random = new Random();

            foreach (var note in notes)
            {
                int commentCount = random.Next(0, 4); // 0-3 комментария
                for (int i = 0; i < commentCount; i++)
                {
                    var user = users.ElementAt(random.Next(users.Count()));
                    var comment = new Comment
                    {
                        Uid = Guid.NewGuid(),
                        User = user,
                        Note = note,
                        Content = GetRandomComment(),
                        CreatedDate = note.CreatedDate.AddDays(random.Next(1, 10))
                    };
                    comments.Add(comment);
                }
            }

            return comments;
        }

        // Вспомогательные методы
        private string GetRandomFeature()
        {
            var features = new List<string>
            {
                "белое пятно на груди",
                "длинный пушистый хвост",
                "короткая блестящая шерсть",
                "голубые глаза",
                "хромает на заднюю лапу",
                "чёрные уши",
                "пятнистый окрас",
                "белая шерсть с рыжими пятнами"
            };
            return features[new Random().Next(features.Count)];
        }

        private string GetRandomComment()
        {
            var comments = new List<string>
            {
                "Видел похожее животное вчера в парке.",
                "Похоже, знаю, где оно может быть.",
                "Оставил еду у подъезда, проверяйте.",
                "Животное было с человеком в синей куртке.",
                "Попробую поискать его сегодня вечером.",
                "Позвоните мне, кажется, видел его!",
                "Он гулял возле школы."
            };
            return comments[new Random().Next(comments.Count)];
        }
    }
}