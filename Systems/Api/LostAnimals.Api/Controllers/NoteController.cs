namespace LostAnimals.Api.Controllers;

using Asp.Versioning;
using AutoMapper;
using LostAnimals.Api.Controllers.Models.Note;
using LostAnimals.Common.Security;
using LostAnimals.Services.Logger;
using LostAnimals.Services.Notes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "LostAnimals.API")]
[Route("v{version:apiVersion}/[controller]")]
public class NoteController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly INoteService noteService;
    private readonly IMapper mapper;

    public NoteController(IAppLogger logger, INoteService noteService, IMapper mapper)
    {
        this.logger = logger;
        this.noteService = noteService;
        this.mapper = mapper;
    }

    [HttpGet("")]
    public async Task<IEnumerable<NoteViewModel>> GetAll()
    {
        var notes = await noteService.GetAll();

        IEnumerable<NoteViewModel> result = notes.Select(mapper.Map<NoteViewModel>);

        return result;
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var note = await noteService.GetById(id);

        if (note == null)
            return NotFound();

        var result = mapper.Map<NoteViewModel>(note);

        return Ok(result);
    }

    [HttpPost("")]
    [Authorize(AppScopes.NotesWrite)]
    public async Task<NoteViewModel> Create(CreateNoteViewModel request)
    {
        var requestModel = mapper.Map<CreateNoteModel>(request);

        var result = await noteService.Create(requestModel);

        return mapper.Map<NoteViewModel>(result);
    }

    [HttpPut("{id:Guid}")]
    [Authorize(AppScopes.NotesWrite)]
    public async Task Update([FromRoute] Guid id, UpdateNoteViewModel request)
    {
        var requestModel = mapper.Map<UpdateNoteModel>(request);

        await noteService.Update(id, requestModel);
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(AppScopes.NotesWrite)]
    public async Task Delete([FromRoute] Guid id)
    {
        await noteService.Delete(id);
    }

    [HttpPost("{id:Guid}/Photo/upload")]
    [Authorize(AppScopes.NotesWrite)]
    public async Task<IActionResult> UploadNotePhoto([FromRoute] Guid id, IFormFile file)
    {
        await noteService.UploadPhoto(id, file);
        return Ok();
    }
}
