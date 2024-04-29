using MediatR;
using VideoHosting.Api.Application.Videos.Models;
using VideoHosting.Api.Common;
using VideoHosting.Common.Constants;
using VideoHosting.Common.Responses;
using VideoHosting.Data.Entities;
using VideoHosting.Database.Abstraction;

namespace VideoHosting.Api.Application.Videos.Queries;

public class GetVideosByName : IRequest<Response<IEnumerable<VideoModel>>>
{
    public string? VideoName { get; }
    public string LoggedInUserId { get; }

    public GetVideosByName(string? videoName, string loggedInUserId)
    {
        VideoName = videoName;
        LoggedInUserId = loggedInUserId;
    }

    public class Handler : BaseHandler<GetVideosByName, IEnumerable<VideoModel>>
    {
        public Handler(IUnitOfWork unit) : base(unit)
        {
        }

        public override async Task<Response<IEnumerable<VideoModel>>> Handle(GetVideosByName request, CancellationToken cancellationToken)
        {
            var videos = string.IsNullOrWhiteSpace(request.VideoName) 
                ? (await Unit.VideoRepository.GetVideos()).ToList() ?? new List<Video>()
                : (await Unit.VideoRepository.GetVideosByName(request.VideoName)).ToList() ?? new List<Video>();
            videos = videos.OrderByDescending(x => x.DayOfCreation).ToList();
            
            var videoModels = videos.Select(v => v.MapToVideoModel())
                .ToList();

            foreach (var video in videoModels)
            {
                video.PhotoPath = Unit.AppSwitchRepository.GetValue(AppSwitchConstants.VideoPhotoKey) + video.PhotoPath;
                video.VideoPath = Unit.AppSwitchRepository.GetValue(AppSwitchConstants.VideoKey) + video.VideoPath;
            }
            
            return Success(videoModels);
        }
    }
}
