using Hellang.Middleware.ProblemDetails;
using MediatR;
using VideoHosting.Api.Application.Videos.Models;
using VideoHosting.Api.Common;
using VideoHosting.Common.Constants;
using VideoHosting.Common.Responses;
using VideoHosting.Database.Abstraction;

namespace VideoHosting.Api.Application.Videos.Queries;

public class GetLikedVideosQuery : IRequest<Response<IEnumerable<VideoModel>>>
{
    public string UserId { get; }

    public GetLikedVideosQuery(string userId)
    {
        UserId = userId;
    }

    public class Handler : BaseHandler<GetLikedVideosQuery, IEnumerable<VideoModel>>
    {
        public Handler(IUnitOfWork unit) : base(unit)
        {
        }

        public override async Task<Response<IEnumerable<VideoModel>>> Handle(GetLikedVideosQuery request, CancellationToken cancellationToken)
        {
            var user = await Unit.UserRepository.GetUserById(request.UserId);
            if (user == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status404NotFound, "User not found");
            }

            var videoModels = user.Likes.Select(x => x.Video.MapToVideoModel())
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
