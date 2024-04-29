using MediatR;
using Microsoft.AspNetCore.Mvc;
using VideoHosting.Api.Application.Videos.Commands;
using VideoHosting.Api.Application.Videos.Models;
using VideoHosting.Api.Application.Videos.Queries;
using VideoHosting.Api.Common;

namespace VideoHosting.Api.Application.Videos;
[Route("api/[controller]")]
[ApiController]
public class VideoController : BaseController
{
    public VideoController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [Route("video")]
    public async Task<IActionResult> AddVideo([FromQuery]VideoApplyModel model)
    {
        var files = HttpContext.Request.Form.Files;
        var result = await Mediator.Send(new CreateVideoCommand(model, files), HttpContext.RequestAborted);
        return Result(result);
    }

    [HttpDelete]
    [Route("video/{id}")]
    public async Task<IActionResult> DeleteVideo(Guid id)
    {
        var result = await Mediator.Send(new DeleteVideoCommand(id, User.Identity?.Name ?? string.Empty, User.IsInRole("Admin") || User.IsInRole("GlobalAdmin")), HttpContext.RequestAborted);
        return Result(result);
    }

    [HttpGet]
    [Route("video/{id}")]
    public async Task<IActionResult> GetVideoById(Guid id)
    {
        var result = await Mediator.Send(new GetVideoByIdQuery(id, User.Identity?.Name ?? string.Empty), HttpContext.RequestAborted);
        
        await Mediator.Send(new AddVideoViewCommand(id), HttpContext.RequestAborted);
        
        return Result(result);
    }

    [HttpGet]
    [Route("video/user/{id}")]
    public async Task<IActionResult> GetVideosOfUser(string id)
    {
        var result = await Mediator.Send(new GetUserVideosQuery(id), HttpContext.RequestAborted);
        return Result(result);
    }

    [HttpGet]
    [Route("video")]
    public async Task<IActionResult> GetVideosSubscribers()
    {
        var result = await Mediator.Send(new GetSubscribersVideosQuery(User.Identity?.Name ?? string.Empty), HttpContext.RequestAborted);
        return Result(result);
    }

    [HttpGet]
    [Route("video/liked/{id}")]
    public async Task<IActionResult> GetLikedVideos(string id)
    {
        var result = await Mediator.Send(new GetLikedVideosQuery(id), HttpContext.RequestAborted);
        return Result(result);
    }

    [HttpGet]
    [Route("video/disliked/{id}")]
    public async Task<IActionResult> GetDislikedVideos(string id)
    {
        var result = await Mediator.Send(new GetDislikedVideosQuery(id), HttpContext.RequestAborted);
        return Result(result);
    }

    //[HttpGet]
    //[Route("allVideos")]
    //public async Task<ActionResult> GetVideo()
    //{
    //    IEnumerable<VideoDto> videos = await _videoService.GetVideos(User.Identity.Name);
    //    return Ok(videos);
    //}

    [HttpGet]
    [Route("video/search")]
    public async Task<IActionResult> GetVideosName([FromQuery] string? name)
    {
        var result = await Mediator.Send(new GetVideosByName(name, User.Identity?.Name ?? string.Empty), HttpContext.RequestAborted);
        return Result(result);
    }


    [HttpPut]
    [Route("like/{id}")]
    public async Task<IActionResult> PutLike(Guid id)
    {
        var result = await Mediator.Send(new PutLikeCommand(id, User.Identity?.Name ?? string.Empty), HttpContext.RequestAborted);
        return Result(result);
    }

    [HttpPut]
    [Route("dislike/{id}")]
    public async Task<IActionResult> PutDislike(Guid id)
    {
        var result = await Mediator.Send(new PutDislikeCommand(id, User.Identity?.Name ?? string.Empty), HttpContext.RequestAborted);
        return Result(result);
    }
}
