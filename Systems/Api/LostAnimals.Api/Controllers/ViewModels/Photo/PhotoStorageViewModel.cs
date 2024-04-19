using AutoMapper;
using LostAnimals.Services.PhotoService.PhotoStorages;

namespace LostAnimals.Api.Controllers.ViewModels.Photo;

public class PhotoStorageViewModel
{
    public Guid Id { get; set; }
    public string PhotoName { get; set; }
}

public class PhotoStorageViewModelProfile : Profile
{
    public PhotoStorageViewModelProfile()
    {
        CreateMap<PhotoStorageModel, PhotoStorageViewModel>();
    }
}