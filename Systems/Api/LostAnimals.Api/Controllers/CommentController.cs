using Asp.Versioning;
using AutoMapper;
using LostAnimals.Api.Controllers.Models.Comment;
using LostAnimals.Common.Security;
using LostAnimals.Services.Comments;
using LostAnimals.Services.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LostAnimals.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "LostAnimals.API")]
[Route("v{version:apiVersion}/[controller]")]
public class CommentController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly ICommentService commentService;
    private readonly IMapper mapper;

    public CommentController(IAppLogger logger, ICommentService commentService, IMapper mapper)
    {
        this.logger = logger;
        this.commentService = commentService;
        this.mapper = mapper;
    }

    [HttpGet("")]
    public async Task<IEnumerable<CommentViewModel>> GetAll()
    {
        var comments = await commentService.GetAll();

        IEnumerable<CommentViewModel> result = comments.Select(mapper.Map<CommentViewModel>);

        return result;
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var comment = await commentService.GetById(id);

        if (comment == null)
            return NotFound();

        var result = mapper.Map<CommentViewModel>(comment);

        return Ok(result);
    }

    [HttpPost("")]
    [Authorize(AppScopes.CommentsWrite)]
    public async Task<CommentViewModel> Create(CreateCommentViewModel request)
    {
        var requestModel = mapper.Map<CreateCommentModel>(request);

        var result = await commentService.Create(requestModel);

        return mapper.Map<CommentViewModel>(result);
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(AppScopes.CommentsWrite)]
    public async Task Delete([FromRoute] Guid id)
    {
        await commentService.Delete(id);
    }

    [HttpPost("{id:Guid}/photos/upload")]
    [Authorize(AppScopes.CommentsWrite)]
    public async Task<IActionResult> UploadCommentPhoto([FromRoute] Guid id, IFormFile file)
    {
        await commentService.UploadPhoto(id, file);
        return Ok();
    }
}
