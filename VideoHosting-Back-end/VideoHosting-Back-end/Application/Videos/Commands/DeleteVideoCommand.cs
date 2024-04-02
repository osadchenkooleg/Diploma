using Hellang.Middleware.ProblemDetails;
using MediatR;
using VideoHosting_Back_end.Application.Videos.Helpers;
using VideoHosting_Back_end.Common.Common;
using VideoHosting_Back_end.Database.Abstraction;

namespace VideoHosting_Back_end.Application.Videos.Commands;

public class DeleteVideoCommand : IRequest<Response<Unit>>
{
    public Guid VideoId { get; }
    public string LoggedInUserId { get; }
    public bool IsAdmin { get; }

    public DeleteVideoCommand(Guid videoId, string loggedInUserId, bool isAdmin)
    {
        VideoId = videoId;
        LoggedInUserId = loggedInUserId;
        IsAdmin = isAdmin;
    }

    public class Handler : BaseHandler<DeleteVideoCommand, Unit>
    {
        private const string VideosPhotos = "UsersContent/VideosPhotos/";
        private const string UsersVideos = "UsersContent/UsersVideos/";

        public Handler(IUnitOfWork unit) : base(unit)
        {
        }

        public override async Task<Response<Unit>> Handle(DeleteVideoCommand request, CancellationToken cancellationToken)
        {
            var videoModel = await VideoHelper.GetVideoByIdWithCorrectPath(request.VideoId, request.LoggedInUserId, Unit);
            if (videoModel.UserId != request.LoggedInUserId && !request.IsAdmin)
            {
                throw new ProblemDetailsException(StatusCodes.Status400BadRequest, "You do not have permission");
            }
            
            var video = await Unit.VideoRepository.GetVideoById(request.VideoId);
            if (video == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status404NotFound, "Video not found");
            }
            Unit.VideoRepository.RemoveVideo(video);
            await Unit.SaveAsync();

            File.Delete(Path.Combine(Directory.GetCurrentDirectory(), VideosPhotos + videoModel.PhotoPath));
            File.Delete(Path.Combine(Directory.GetCurrentDirectory(), UsersVideos + videoModel.VideoPath));
            
            return Success(new Unit());
        }
    }
}
