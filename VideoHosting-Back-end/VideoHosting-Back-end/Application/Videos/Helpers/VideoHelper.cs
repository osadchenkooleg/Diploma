using Hellang.Middleware.ProblemDetails;
using VideoHosting_Back_end.Application.Videos.Models;
using VideoHosting_Back_end.Common;
using VideoHosting_Back_end.Common.Constants;
using VideoHosting_Back_end.Database.Abstraction;

namespace VideoHosting_Back_end.Application.Videos.Helpers;

public static class VideoHelper
{
    public static async Task<VideoModel> GetVideoByIdWithCorrectPath(Guid videoId, string userId, IUnitOfWork unit)
    {
        var user = await unit.UserRepository.GetUserById(userId);
        if (user == null)
        {
            throw new ProblemDetailsException(StatusCodes.Status404NotFound, "User not found");
        }

        var video = await unit.VideoRepository.GetVideoById(videoId);
        if (video == null)
        {
            throw new ProblemDetailsException(StatusCodes.Status404NotFound, "Video not found");
        }

        var videoModel = video.MapToVideoModel();

        videoModel.PhotoPath = unit.AppSwitchRepository.GetValue(AppSwitchConstants.VideoPhotoKey) + videoModel.PhotoPath;
        videoModel.VideoPath = unit.AppSwitchRepository.GetValue(AppSwitchConstants.VideoKey) + videoModel.VideoPath;
        videoModel.Liked = video.Likes.FirstOrDefault(x => x.User == user) == null;
        videoModel.Disliked = video.Dislikes.FirstOrDefault(x => x.User == user) == null;

        return videoModel;
    }
}
