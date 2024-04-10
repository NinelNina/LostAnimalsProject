using Asp.Versioning;
using LostAnimals.Services.Logger;
using LostAnimals.Services.PhotoGalleries;
using Microsoft.AspNetCore.Mvc;

namespace LostAnimals.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "LostAnimals.API")]
[Route("v{version:apiVersion}/[controller]")]
public class PhotoGalleryController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly IPhotoGalleryService photoGalleryService;

    public PhotoGalleryController(IAppLogger logger, IPhotoGalleryService photoGalleryService)
    {
        this.logger = logger;
        this.photoGalleryService = photoGalleryService;
    }

    [HttpGet("")]
    public async Task<IEnumerable<PhotoGalleryModel>> GetAll()
    {
        var result = await photoGalleryService.GetAll();

        return result;
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await photoGalleryService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("")]
    public async Task<PhotoGalleryModel> Create(CreatePhotoGalleryModel request)
    {
        var result = await photoGalleryService.Create(request);

        return result;
    }

    [HttpDelete("{id:Guid}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await photoGalleryService.Delete(id);
    }
}
