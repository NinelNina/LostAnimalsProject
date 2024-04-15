using Asp.Versioning;
using LostAnimals.Services.Logger;
using LostAnimals.Services.Breeds;
using Microsoft.AspNetCore.Mvc;
using LostAnimals.Common.Security;
using Microsoft.AspNetCore.Authorization;

namespace LostAnimals.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "LostAnimals.API")]
[Route("v{version:apiVersion}/[controller]")]
public class BreedController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly IBreedService breedService;

    public BreedController(IAppLogger logger, IBreedService breedService)
    {
        this.logger = logger;
        this.breedService = breedService;
    }

    [HttpGet("")]
    public async Task<IEnumerable<BreedModel>> GetAll()
    {
        var result = await breedService.GetAll();

        return result;
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await breedService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("")]
    [Authorize(AppScopes.BreedsWrite)]
    public async Task<BreedModel> Create(CreateBreedModel request)
    {
        var result = await breedService.Create(request);

        return result;
    }

    [HttpPut("{id:Guid}")]
    [Authorize(AppScopes.BreedsWrite)]
    public async Task Update([FromRoute] Guid id, UpdateBreedModel request)
    {
        await breedService.Update(id, request);
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(AppScopes.BreedsWrite)]
    public async Task Delete([FromRoute] Guid id)
    {
        await breedService.Delete(id);
    }
}
