using MediatR;
using VideoHosting_Back_end.Application.Videos.Models;
using VideoHosting_Back_end.Common;
using VideoHosting_Back_end.Common.Common;
using VideoHosting_Back_end.Common.Constants;
using VideoHosting_Back_end.Data.Entities;
using VideoHosting_Back_end.Database.Abstraction;

namespace VideoHosting_Back_end.Application.Videos.Queries;

public class GetVideosByName : IRequest<Response<IEnumerable<VideoModel>>>
{
    public string VideoName { get; }
    public string LoggedInUserId { get; }

    public GetVideosByName(string videoName, string loggedInUserId)
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
            var videos = (await Unit.VideoRepository.GetVideosByName(request.VideoName)).ToList() ?? new List<Video>();
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
