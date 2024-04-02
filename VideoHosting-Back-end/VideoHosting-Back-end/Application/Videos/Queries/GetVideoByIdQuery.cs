using MediatR;
using VideoHosting_Back_end.Application.Videos.Helpers;
using VideoHosting_Back_end.Application.Videos.Models;
using VideoHosting_Back_end.Common.Common;
using VideoHosting_Back_end.Database.Abstraction;

namespace VideoHosting_Back_end.Application.Videos.Queries;

public class GetVideoByIdQuery : IRequest<Response<VideoModel>>
{
    public Guid VideoId { get; }
    public string LoggedInUserId { get; }

    public GetVideoByIdQuery(Guid videoId, string loggedInUserId)
    {
        VideoId = videoId;
        LoggedInUserId = loggedInUserId;
    }

    public class Handler : BaseHandler<GetVideoByIdQuery, VideoModel>
    {
        public Handler(IUnitOfWork unit) : base(unit)
        {
        }

        public override async Task<Response<VideoModel>> Handle(GetVideoByIdQuery request, CancellationToken cancellationToken)
        {
            var videoModel = await VideoHelper.GetVideoByIdWithCorrectPath(request.VideoId, request.LoggedInUserId, Unit);

            return Success(videoModel);
        }
    }
}
