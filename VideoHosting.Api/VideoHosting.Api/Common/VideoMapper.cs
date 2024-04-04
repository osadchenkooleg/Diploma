using VideoHosting.Api.Application.Videos.Models;
using VideoHosting.Data.Entities;

namespace VideoHosting.Api.Common;

public static class VideoMapper
{
    public static VideoModel MapToVideoModel(this Video video)
    {
        return new VideoModel
        {
            Id = video.Id,
            UserId = video.User.Id,
            Name = video.Name,
            DayOfCreation = video.DayOfCreation,
            Views = video.Views,
            VideoPath = video.VideoPath,
            PhotoPath = video.PhotoPath,
            Likes = video.Likes.Count,
            Dislikes = video.Dislikes.Count,
            Description = video.Description
        };
    }
}
