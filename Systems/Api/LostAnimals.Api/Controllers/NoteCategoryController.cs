using Asp.Versioning;
using LostAnimals.Common.Security;
using LostAnimals.Services.AnimalKinds;
using LostAnimals.Services.Logger;
using LostAnimals.Services.NoteCategories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LostAnimals.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "LostAnimals.API")]
    [Route("v{version:apiVersion}/[controller]")]
    public class NoteCategoryController : ControllerBase
    {
        private readonly IAppLogger logger;
        private readonly INoteCategoryService noteCategoryService;

        public NoteCategoryController(IAppLogger logger, INoteCategoryService noteCategoryService)
        {
            this.logger = logger;
            this.noteCategoryService = noteCategoryService;
        }

        [HttpGet("")]
        public async Task<IEnumerable<NoteCategoryModel>> GetAll()
        {
            var result = await noteCategoryService.GetAll();

            return result;
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await noteCategoryService.GetById(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("")]
        [Authorize(AppScopes.NoteCategoriesWrite)]
        public async Task<NoteCategoryModel> Create(CreateNoteCategoryModel request)
        {
            var result = await noteCategoryService.Create(request);

            return result;
        }

        [HttpPut("{id:Guid}")]
        [Authorize(AppScopes.NoteCategoriesWrite)]
        public async Task Update([FromRoute] Guid id, UpdateNoteCategoryModel request)
        {
            await noteCategoryService.Update(id, request);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(AppScopes.NoteCategoriesWrite)]
        public async Task Delete([FromRoute] Guid id)
        {
            await noteCategoryService.Delete(id);
        }
    }
}
