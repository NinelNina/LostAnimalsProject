using Asp.Versioning;
using LostAnimals.Common.Security;
using LostAnimals.Services.PhotoService;
using LostAnimals.Services.PhotoService.PhotoStorages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LostAnimals.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "LostAnimals.API")]
    [Route("v{version:apiVersion}/[controller]")]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService photoService;
        private readonly Serilog.ILogger logger;

        public PhotoController(IPhotoService photoService, Serilog.ILogger logger)
        {
            this.photoService = photoService;
            this.logger = logger;
        }

        [HttpGet("{galleryId:Guid}")]
        public async Task<IActionResult> GetPhotosByGalleryId([FromRoute] Guid galleryId)
        {
            var photos = await photoService.GetPhotosByGalleryId(galleryId);

            return Ok(photos);
        }

        [HttpDelete("deletePhoto/{photoId:Guid}")]
        [Authorize(AppScopes.PhotosWrite)]
        public async Task DeletePhoto([FromRoute] Guid photoId)
        {
            await photoService.DeletePhoto(photoId);
        }
    }
}
