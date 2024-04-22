using Asp.Versioning;
using AutoMapper;
using LostAnimals.ViewModels.Breed;
using LostAnimals.Common.Security;
using LostAnimals.Services.Breeds;
using LostAnimals.Services.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LostAnimals.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "LostAnimals.API")]
[Route("v{version:apiVersion}/[controller]")]
public class BreedController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly IBreedService breedService;
    private readonly IMapper mapper;

    public BreedController(IAppLogger logger, IBreedService breedService, IMapper mapper)
    {
        this.logger = logger;
        this.breedService = breedService;
        this.mapper = mapper;
    }

    [HttpGet("")]
    public async Task<IEnumerable<BreedViewModel>> GetAll()
    {
        var breeds = await breedService.GetAll();

        IEnumerable<BreedViewModel> result = breeds.Select(mapper.Map<BreedViewModel>);

        return result;
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var breed = await breedService.GetById(id);

        if (breed == null)
            return NotFound();

        var result = mapper.Map<BreedViewModel>(breed);

        return Ok(result);
    }

    [HttpPost("")]
    [Authorize(AppScopes.BreedsWrite)]
    public async Task<BreedViewModel> Create(CreateBreedViewModel request)
    {
        var requestModel = mapper.Map<CreateBreedModel>(request);

        var result = await breedService.Create(requestModel);

        return mapper.Map<BreedViewModel>(result);
    }

    [HttpPut("{id:Guid}")]
    [Authorize(AppScopes.BreedsWrite)]
    public async Task Update([FromRoute] Guid id, UpdateBreedViewModel request)
    {
        var requestModel = mapper.Map<UpdateBreedModel>(request);

        await breedService.Update(id, requestModel);
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(AppScopes.BreedsWrite)]
    public async Task Delete([FromRoute] Guid id)
    {
        await breedService.Delete(id);
    }
}
