﻿using Asp.Versioning;
using AutoMapper;
using LostAnimals.ViewModels.NoteCategory;
using LostAnimals.Common.Security;
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

            var result = mapper.Map<NoteCategoryViewModel>(noteCategory);

            return Ok(result);
        }        
        
        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName([FromRoute] string name)
        {
            var noteCategory = await noteCategoryService.GetByName(name);

            if (noteCategory == null)
                return NotFound();

            var result = mapper.Map<NoteCategoryViewModel>(noteCategory);

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

        [HttpDelete("{id:Guid}")]
        [Authorize(AppScopes.NoteCategoriesWrite)]
        public async Task Delete([FromRoute] Guid id)
        {
            await noteCategoryService.Delete(id);
        }
    }
}
