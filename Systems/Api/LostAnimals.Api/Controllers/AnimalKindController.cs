using Asp.Versioning;
using LostAnimals.Services.Logger;
using LostAnimals.Services.AnimalKinds;
using Microsoft.AspNetCore.Mvc;
using LostAnimals.Services.Notes;

namespace LostAnimals.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Product")]
[Route("v{version:apiVersion}/[controller]")]
public class AnimalKindController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly IAnimalKindService animalKindService;

    public AnimalKindController(IAppLogger logger, IAnimalKindService animalKindService)
    {
        this.logger = logger;
        this.animalKindService = animalKindService;
    }

    [HttpGet("")]
    public async Task<IEnumerable<AnimalKindModel>> GetAll()
    {
        var result = await animalKindService.GetAll();

        return result;
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await animalKindService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("")]
    public async Task<AnimalKindModel> Create(CreateAnimalKindModel request)
    {
        var result = await animalKindService.Create(request);

        return result;
    }

    [HttpPut("{id:Guid}")]
    public async Task Update([FromRoute] Guid id, UpdateAnimalKindModel request)
    {
        await animalKindService.Update(id, request);
    }

    [HttpDelete("{id:Guid}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await animalKindService.Delete(id);
    }
}