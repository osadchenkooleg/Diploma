using VideoHosting_Back_end.Application.Videos.Models;
using VideoHosting_Back_end.Data.Entities;

namespace VideoHosting_Back_end.Common;

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
