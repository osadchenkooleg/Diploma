using Hellang.Middleware.ProblemDetails;
using MediatR;
using VideoHosting_Back_end.Application.Videos.Models;
using VideoHosting_Back_end.Common;
using VideoHosting_Back_end.Common.Common;
using VideoHosting_Back_end.Common.Constants;
using VideoHosting_Back_end.Database.Abstraction;

namespace VideoHosting_Back_end.Application.Videos.Queries;

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
