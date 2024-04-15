using Asp.Versioning;
using LostAnimals.Context.Entities;
using LostAnimals.Services.Notes;
using LostAnimals.Services.PhotoService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LostAnimals.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "LostAnimals.API")]
    [Route("v{version:apiVersion}/[controller]")]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService photoService;

        public PhotoController(IPhotoService photoService)
        {
            this.photoService = photoService;
        }

        [HttpGet("{galleryId:Guid}/photos")]
        public async Task<IActionResult> GetPhotosByGalleryId([FromRoute] Guid galleryId)
        {
            var photos = await photoService.GetPhotosByGalleryId(galleryId);
            return Ok(photos);
        }
    }
}
