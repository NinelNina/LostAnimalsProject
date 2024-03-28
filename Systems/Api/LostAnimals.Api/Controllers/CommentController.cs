﻿using Asp.Versioning;
using LostAnimals.Services.Comments;
using LostAnimals.Services.Logger;
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

    public CommentController(IAppLogger logger, ICommentService commentService)
    {
        this.logger = logger;
        this.commentService = commentService;
    }

    [HttpGet("")]
    public async Task<IEnumerable<CommentModel>> GetAll()
    {
        var result = await commentService.GetAll();

        return result;
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await commentService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("")]
    public async Task<CommentModel> Create(CreateCommentModel request)
    {
        var result = await commentService.Create(request);

        return result;
    }

    [HttpDelete("{id:Guid}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await commentService.Delete(id);
    }
}
