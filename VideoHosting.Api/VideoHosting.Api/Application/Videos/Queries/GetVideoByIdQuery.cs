using MediatR;
using VideoHosting.Api.Application.Videos.Helpers;
using VideoHosting.Api.Application.Videos.Models;
using VideoHosting.Api.Common;
using VideoHosting.Common.Responses;
using VideoHosting.Database.Abstraction;

namespace VideoHosting.Api.Application.Videos.Queries;

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
