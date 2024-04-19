using Asp.Versioning;
using AutoMapper;
using LostAnimals.Api.Controllers.Models.Comment;
using LostAnimals.Api.Controllers.Models.NoteCategory;
using LostAnimals.Common.Security;
using LostAnimals.Context.Entities;
using LostAnimals.Services.Comments;
using LostAnimals.Services.Logger;
using LostAnimals.Services.NoteCategories;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IMapper mapper;

        public NoteCategoryController(IAppLogger logger, INoteCategoryService noteCategoryService, IMapper mapper)
        {
            this.logger = logger;
            this.noteCategoryService = noteCategoryService;
            this.mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IEnumerable<NoteCategoryViewModel>> GetAll()
        {
            var noteCategories = await noteCategoryService.GetAll();

            IEnumerable<NoteCategoryViewModel> result = noteCategories.Select(mapper.Map<NoteCategoryViewModel>);

            return result;
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var noteCategory = await noteCategoryService.GetById(id);

            if (noteCategory == null)
                return NotFound();

            var result = mapper.Map<CommentViewModel>(noteCategory);

            return Ok(result);
        }

        [HttpPost("")]
        [Authorize(AppScopes.NoteCategoriesWrite)]
        public async Task<NoteCategoryViewModel> Create(CreateNoteCategoryViewModel request)
        {
            var requestModel = mapper.Map<CreateNoteCategoryModel>(request);

            var result = await noteCategoryService.Create(requestModel);

            return mapper.Map<NoteCategoryViewModel>(result);
        }

/*        [HttpPut("{id:Guid}")]
        [Authorize(AppScopes.NoteCategoriesWrite)]
        public async Task Update([FromRoute] Guid id, UpdateNoteCategoryViewModel request)
        {
            await noteCategoryService.Update(id, request);
        }*/

        [HttpDelete("{id:Guid}")]
        [Authorize(AppScopes.NoteCategoriesWrite)]
        public async Task Delete([FromRoute] Guid id)
        {
            await noteCategoryService.Delete(id);
        }
    }
}
