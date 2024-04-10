using Asp.Versioning;
using LostAnimals.Services.PhotoStorages;
using LostAnimals.Services.Logger;
using Microsoft.AspNetCore.Mvc;

namespace LostAnimals.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "LostAnimals.API")]
[Route("v{version:apiVersion}/[controller]")]
public class PhotoStorageController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly IPhotoStorageService photoStorageService;

    public PhotoStorageController(IAppLogger logger, IPhotoStorageService photoStorageService)
    {
        this.logger = logger;
        this.photoStorageService = photoStorageService;
    }

    [HttpGet("")]
    public async Task<IEnumerable<PhotoStorageModel>> GetAll()
    {
        var result = await photoStorageService.GetAll();

        return result;
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await photoStorageService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("")]
    public async Task<PhotoStorageModel> Create(CreatePhotoStorageModel request)
    {
        var result = await photoStorageService.Create(request);

        return result;
    }

    [HttpDelete("{id:Guid}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await photoStorageService.Delete(id);
    }
}
