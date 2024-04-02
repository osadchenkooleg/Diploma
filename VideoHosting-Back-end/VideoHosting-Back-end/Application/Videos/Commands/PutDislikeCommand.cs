using Hellang.Middleware.ProblemDetails;
using MediatR;
using VideoHosting_Back_end.Common.Common;
using VideoHosting_Back_end.Database.Abstraction;

namespace VideoHosting_Back_end.Application.Videos.Commands;

public class PutDislikeCommand : IRequest<Response<Unit>>
{
    public Guid VideoId { get; }
    public string LoggedInUserId { get; }

    public PutDislikeCommand(Guid videoId, string loggedInUserId)
    {
        VideoId = videoId;
        LoggedInUserId = loggedInUserId;
    }
    public class Handler : BaseHandler<PutDislikeCommand, Unit>
    {
        public Handler(IUnitOfWork unit) : base(unit)
        {
        }

        public override async Task<Response<Unit>> Handle(PutDislikeCommand request, CancellationToken cancellationToken)
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

            user.AddDislike(video);
            await Unit.SaveAsync();

            return Success(new Unit());
        }
    }
}
