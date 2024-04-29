using Hellang.Middleware.ProblemDetails;
using MediatR;
using VideoHosting.Api.Application.Videos.Models;
using VideoHosting.Api.Common;
using VideoHosting.Common.Constants;
using VideoHosting.Common.Responses;
using VideoHosting.Data.Entities;
using VideoHosting.Database.Abstraction;

namespace VideoHosting.Api.Application.Videos.Commands;

public class CreateVideoCommand : IRequest<Response<VideoModel>>
{
    public VideoApplyModel Model { get; }
    public IFormFileCollection Files { get; }

    public CreateVideoCommand(VideoApplyModel model, IFormFileCollection files)
    {
        Model = model;
        Files = files;
    }
    public class Handler : BaseHandler<CreateVideoCommand, VideoModel>
    {
        protected static string TargetDir => $@"{Directory.GetCurrentDirectory()}\wwwroot\";
        private static readonly string[] JpgFileFormats = new[] {".JPG",  ".jpg"};
        private const string Png = ".png";
        private const string Mp4 = ".mp4";

        private const string VideoImagePath = "videoimages";
        private const string VideoPath = "videos";


        public Handler(IUnitOfWork unit) : base(unit)
        {
        }

        public override async Task<Response<VideoModel>> Handle(CreateVideoCommand request, CancellationToken cancellationToken)
        {
            var files = request.Files;
            var model = request.Model;
            
            var photoPath = string.Empty;
            var videoPath = string.Empty;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            var user = await Unit.UserRepository.GetUserById(model.UserId);
            if (user == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status404NotFound, "Logged in user not found");
            }

            for (var i = 0; i < 2; i++)
            {
                var save = Guid.NewGuid().ToString();
                
                if (JpgFileFormats.Any(f => files[i].FileName.Contains(f)))
                {
                    photoPath = save + ".jpg";
                    
                    var base64 = ResolveIFormFileToBase64(files[i]).Split(',').Last();

                    var wwwPath = CreatePath( VideoImagePath, photoPath);
                    var bytes = Convert.FromBase64String(base64.Split(',').Last());

                    await using var fs = new FileStream(wwwPath, FileMode.Create);
                    await fs.WriteAsync(bytes, cancellationToken);
                }
                if (files[i].FileName.Contains(Png))
                {
                    photoPath = save + Png;
                    
                    var base64 = ResolveIFormFileToBase64(files[i]).Split(',').Last();

                    var wwwPath = CreatePath( VideoImagePath, photoPath);
                    var bytes = Convert.FromBase64String(base64.Split(',').Last());

                    await using var fs = new FileStream(wwwPath, FileMode.Create);
                    await fs.WriteAsync(bytes, cancellationToken);
                }
                if (files[i].FileName.Contains(Mp4))
                {
                    videoPath = save + Mp4;

                    var base64 = ResolveIFormFileToBase64(files[i]).Split(',').Last();

                    var wwwPath = CreatePath( VideoPath, videoPath);
                    var bytes = Convert.FromBase64String(base64.Split(',').Last());

                    await using var fs = new FileStream(wwwPath, FileMode.Create);
                    await fs.WriteAsync(bytes, cancellationToken);
                }
            }
            
            var video = new Video
            {
                Name = request.Model.Name,
                Description = request.Model.Description,
                Views = 0,
                User = user,
                PhotoPath = photoPath,
                VideoPath = videoPath,
                DayOfCreation = DateTime.Now,
            };

            await Unit.VideoRepository.AddVideo(video);
            await Unit.SaveAsync();

            var videoModel = video.MapToVideoModel();

            videoModel.PhotoPath = Unit.AppSwitchRepository.GetValue(AppSwitchConstants.VideoPhotoKey) + videoModel.PhotoPath;
            videoModel.VideoPath = Unit.AppSwitchRepository.GetValue(AppSwitchConstants.VideoKey) + videoModel.VideoPath;
            videoModel.Liked = video.Likes.FirstOrDefault(x => x.User == user) == null;
            videoModel.Disliked = video.Dislikes.FirstOrDefault(x => x.User == user) == null;

            return Success(videoModel);
        }

        private string CreatePath(string folder ,string fileName) => $"{TargetDir}{folder}\\{fileName}";

        private string ResolveIFormFileToBase64(IFormFile file)
        {
            string result;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                result = Convert.ToBase64String(fileBytes);
            }
            return result;
        }
    }
}
