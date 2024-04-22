using AutoMapper;
using FluentValidation;
using LostAnimals.Services.Notes;

namespace LostAnimals.ViewModels.Note;

public class CreateNoteViewModel
{
    public Guid UserId { get; set; }

    public Guid CategoryId { get; set; }

    public string Title { get; set; }

    public string? AnimalName { get; set; }

    public Guid BreedId { get; set; }

    public string Content { get; set; }

    public Guid? PhotoGalleryId { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public string PhoneNumber { get; set; }

    public DateTime LastSeenDate { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public class CreateNoteModelValidator : AbstractValidator<CreateNoteViewModel>
    {
        public CreateNoteModelValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Поле \"Заголовок\" обязательно для заполнения.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Поле \"Номер телефона\" обязательно для заполнения. Так человек, откликнувшийся на объявление, сможет быстрее с Вами связаться.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Необходимо выбрать категорию объявления.");

            RuleFor(x => x.BreedId)
                .NotEmpty().WithMessage("Укажите породу животного.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Введите текст объявления.")
                .MinimumLength(50).WithMessage("Текст слишком короткий.");

            RuleFor(x => x.LastSeenDate)
                .NotEmpty().WithMessage("Укажите дату пропажи/находки животного.");
        }
    }
}

public class CreateNoteViewModelProfile : Profile
{
    public CreateNoteViewModelProfile()
    {
        CreateMap<CreateNoteViewModel, NoteModel>();
    }
}