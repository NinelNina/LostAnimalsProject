using Asp.Versioning;
using LostAnimals.Services.Logger;
using LostAnimals.Services.AnimalKinds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LostAnimals.Common.Security;
using AutoMapper;
using LostAnimals.Api.Controllers.Models.AnimalKind;

namespace LostAnimals.Api.Controllers;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "LostAnimals.API")]
[Route("v{version:apiVersion}/[controller]")]
public class AnimalKindController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly IAnimalKindService animalKindService;
    private readonly IMapper mapper;

    public AnimalKindController(IAppLogger logger, IAnimalKindService animalKindService, IMapper mapper)
    {
        this.logger = logger;
        this.animalKindService = animalKindService;
        this.mapper = mapper;
    }

    [HttpGet("")]
    [AllowAnonymous]
    public async Task<IEnumerable<AnimalKindViewModel>> GetAll()
    {
        var animalKinds = await animalKindService.GetAll();

        IEnumerable<AnimalKindViewModel> result = animalKinds.Select(animalKind => mapper.Map<AnimalKindViewModel>(animalKind));

        return result;
    }

    [HttpGet("{id:Guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var animalKind = await animalKindService.GetById(id);

        if (animalKind == null)
            return NotFound();

        var result = mapper.Map<AnimalKindViewModel>(animalKind);

        return Ok(result);
    }

    [HttpPost("")]
    [Authorize(AppScopes.AnimalKindsWrite)]
    public async Task<AnimalKindViewModel> Create(CreateAnimalKindViewModel request)
    {
        var requestModel = mapper.Map<CreateAnimalKindModel>(request);

        var result = await animalKindService.Create(requestModel);

        return mapper.Map<AnimalKindViewModel>(result);
    }

    [HttpPut("{id:Guid}")]
    [Authorize(AppScopes.AnimalKindsWrite)]
    public async Task Update([FromRoute] Guid id, UpdateAnimalKindViewModel request)
    {
        var requestModel = mapper.Map<UpdateAnimalKindModel>(request);

        await animalKindService.Update(id, requestModel);
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(AppScopes.AnimalKindsWrite)]
    public async Task Delete([FromRoute] Guid id)
    {
        await animalKindService.Delete(id);
    }
}