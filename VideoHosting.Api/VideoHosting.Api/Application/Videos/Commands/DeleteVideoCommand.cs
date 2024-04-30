using Hellang.Middleware.ProblemDetails;
using MediatR;
using VideoHosting.Api.Application.Videos.Helpers;
using VideoHosting.Api.Common;
using VideoHosting.Common.Responses;
using VideoHosting.Database.Abstraction;

namespace VideoHosting.Api.Application.Videos.Commands;

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
        protected static string TargetDir => $@"{Directory.GetCurrentDirectory()}\wwwroot\";
        private const string VideoImagePath = "videoimages";
        private const string VideoPath = "videos";

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

            if (File.Exists(CreatePath(VideoImagePath, videoModel.PhotoPath)))
                await Task.Run(() => File.Delete(CreatePath(VideoImagePath, videoModel.PhotoPath)));

            if (File.Exists(CreatePath(VideoPath, videoModel.VideoPath)))
                await Task.Run(() => File.Delete(CreatePath(VideoPath, videoModel.VideoPath)));
            
            return Success(new Unit());
        }

        private string CreatePath(string folder ,string fileName) => $"{TargetDir}{folder}\\{fileName}";
    }
}
