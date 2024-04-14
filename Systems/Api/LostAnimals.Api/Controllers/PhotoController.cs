/*using Asp.Versioning;
using LostAnimals.Context.Entities;
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

        [HttpPost("")]
        public async Task<IActionResult> CreatePhotoGallery()
        {
            var gallery = await photoService.CreatePhotoGallery();
            return Ok(gallery);
        }

        [HttpPost("{galleryId}/photos")]
        public async Task<IActionResult> UploadPhoto([FromRoute] Guid galleryId, IFormFile file)
        {
            if (file.Length > 0)
            {
                var photo = await photoService.SavePhoto(galleryId, file);

                return Ok(photo);
            } 
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetPhotoGallery([FromRoute] Guid id)
        {
            var gallery = await photoService.GetPhotoGalleryById(id);

            if (gallery == null)
            {
                return NotFound();
            }

            return Ok(gallery);
        }
    }
}
*/