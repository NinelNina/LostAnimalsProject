namespace LostAnimals.Api.Controllers;

using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using LostAnimals.Services.Notes;
using LostAnimals.Services.Logger;
using Microsoft.AspNetCore.Authorization;
using LostAnimals.Common.Security;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "LostAnimals.API")]
[Route("v{version:apiVersion}/[controller]")]
public class NoteController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly INoteService noteService;

    public NoteController(IAppLogger logger, INoteService noteService)
    {
        this.logger = logger;
        this.noteService = noteService;
    }

    [HttpGet("")]
    public async Task<IEnumerable<NoteModel>> GetAll()
    {
        var result = await noteService.GetAll();

        return result;
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await noteService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("")]
    [Authorize(AppScopes.NotesWrite)]
    public async Task<NoteModel> Create(CreateNoteModel request)
    {
        var result = await noteService.Create(request);

        return result;
    }

    [HttpPut("{id:Guid}")]
    [Authorize(AppScopes.NotesWrite)]
    public async Task Update([FromRoute] Guid id, UpdateNoteModel request)
    {
        await noteService.Update(id, request);
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
