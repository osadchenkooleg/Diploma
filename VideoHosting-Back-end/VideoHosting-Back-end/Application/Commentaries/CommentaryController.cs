using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoHosting_Back_end.Application.Auth.Commands;
using VideoHosting_Back_end.Application.Commentaries.Commands;
using VideoHosting_Back_end.Application.Commentaries.Models;
using VideoHosting_Back_end.Application.Commentaries.Queries;
using VideoHosting_Back_end.Application.Users.Models;
using static System.String;

namespace VideoHosting_Back_end.Application.Commentaries;

[Route("api")]
[ApiController]
public class CommentaryController : BaseController
{
    public CommentaryController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [Route("Commentary/{id}")]
    public async Task<IActionResult> GetCommentariesByVideoId(Guid id)
    {
        var result = await Mediator.Send(new GetByVideoIdQuery(id), HttpContext.RequestAborted);
        return Result(result);
    }

    [HttpPost]
    [Route("Commentary")]
    public async Task<IActionResult> CreateCommentary(CommentaryApplyModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await Mediator.Send(new CreateCommentaryCommand(model), HttpContext.RequestAborted);
            return Result(result);
        }
        return BadRequest();
    }

    [HttpDelete]
    [Route("Commentaries/{id}")]
    public async Task<IActionResult> DeleteCommentary(Guid id)
    {
        var loggedInUser = User.Identity?.Name ?? string.Empty;
        var isAdmin = User.IsInRole("Admin") || User.IsInRole("GlobalAdmin");
        var result = await Mediator.Send(new DeleteCommentaryCommand(id, loggedInUser, isAdmin), HttpContext.RequestAborted);
        return Result(result);
    }
}
