using Hellang.Middleware.ProblemDetails;
using MediatR;
using VideoHosting.Api.Application.Videos.Models;
using VideoHosting.Api.Common;
using VideoHosting.Common.Constants;
using VideoHosting.Common.Responses;
using VideoHosting.Database.Abstraction;

namespace VideoHosting.Api.Application.Videos.Queries;

public class GetUserVideosQuery : IRequest<Response<IEnumerable<VideoModel>>>
{
    public string UserId { get; }

    public GetUserVideosQuery(string userId)
    {
        UserId = userId;
    }
    public class Handler : BaseHandler<GetUserVideosQuery, IEnumerable<VideoModel>>
    {
        public Handler(IUnitOfWork unit) : base(unit)
        {
        }

        public override async Task<Response<IEnumerable<VideoModel>>> Handle(GetUserVideosQuery request, CancellationToken cancellationToken)
        {
            var user = await Unit.UserRepository.GetUserById(request.UserId);
            if (user == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status404NotFound, $"User with id = {request.UserId} does not exist.");
            }

            var videoModels = user.Videos.Select(v => v.MapToVideoModel()).ToList();

            foreach (var video in videoModels)
            {
                video.PhotoPath = Unit.AppSwitchRepository.GetValue(AppSwitchConstants.VideoPhotoKey) + video.PhotoPath;
                video.VideoPath = Unit.AppSwitchRepository.GetValue(AppSwitchConstants.VideoKey) + video.VideoPath;
            }

            return Success(videoModels);
        }
    }
}
