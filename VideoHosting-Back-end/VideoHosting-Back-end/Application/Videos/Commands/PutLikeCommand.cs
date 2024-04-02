using Hellang.Middleware.ProblemDetails;
using MediatR;
using VideoHosting_Back_end.Common.Common;
using VideoHosting_Back_end.Database.Abstraction;

namespace VideoHosting_Back_end.Application.Videos.Commands;

public class PutLikeCommand : IRequest<Response<Unit>>
{
    public Guid VideoId { get; }
    public string LoggedInUserId { get; }

    public PutLikeCommand(Guid videoId, string loggedInUserId)
    {
        VideoId = videoId;
        LoggedInUserId = loggedInUserId;
    }

    public class Handler : BaseHandler<PutLikeCommand, Unit>
    {
        public Handler(IUnitOfWork unit) : base(unit)
        {
        }

        public override async Task<Response<Unit>> Handle(PutLikeCommand request, CancellationToken cancellationToken)
        {
            var user = await Unit.UserRepository.GetUserById(request.LoggedInUserId);
            if (user == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status404NotFound, "User not found");
            }

            var video = await Unit.VideoRepository.GetVideoById(request.VideoId);
            if (video == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status404NotFound, "Video not found");
            }

            user.AddLike(video);
            await Unit.SaveAsync();

            return Success(new Unit());
        }
    }
}
