using Hellang.Middleware.ProblemDetails;
using MediatR;
using VideoHosting_Back_end.Application.Videos.Models;
using VideoHosting_Back_end.Common.Common;
using VideoHosting_Back_end.Data.Entities;
using VideoHosting_Back_end.Database.Abstraction;

namespace VideoHosting_Back_end.Application.Videos.Commands;

public class CreateVideoCommand : IRequest<Response<string[]>>
{
    public VideoApplyModel Model { get; }
    public IFormFileCollection Files { get; }

    public CreateVideoCommand(VideoApplyModel model, IFormFileCollection files)
    {
        Model = model;
        Files = files;
    }
    public class Handler : BaseHandler<CreateVideoCommand, string[]>
    {
        private static readonly string[] JpgFileFormats = new[] {".JPG",  ".jpg"};
        private const string Png = ".png";
        private const string Mp4 = ".mp4";


        public Handler(IUnitOfWork unit) : base(unit)
        {
        }

        public override async Task<Response<string[]>> Handle(CreateVideoCommand request, CancellationToken cancellationToken)
        {
            var files = request.Files;
            var model = request.Model;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "UsersContent");

            for (var i = 0; i < 2; i++)
            {
                var save = Guid.NewGuid().ToString();
                if (JpgFileFormats.Any(f => files[i].FileName.Contains(f)))
                {
                    model.PhotoPath = save + ".jpg";
                    await using var stream = File.Create(Path.Combine(path, "VideosPhotos", model.PhotoPath));
                    await files[i].CopyToAsync(stream, cancellationToken);
                }
                if (files[i].FileName.Contains(Png))
                {
                    model.PhotoPath = save + Png;
                    await using var stream = File.Create(Path.Combine(path, "VideosPhotos", model.PhotoPath));
                    await files[i].CopyToAsync(stream, cancellationToken);
                }
                if (files[i].FileName.Contains(Mp4))
                {
                    model.VideoPath = save + Mp4;
                    await using var stream = File.Create(Path.Combine(path, "VideosPhotos", model.VideoPath));
                    await files[i].CopyToAsync(stream, cancellationToken);
                }
            }

            var user = await Unit.UserRepository.GetUserById(request.Model.UserId);
            if (user == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status404NotFound, "Logged in user not found");
            }

            var video = new Video
            {
                Name = request.Model.Name,
                Description = request.Model.Description,
                Views = 0,
                User = user,
                PhotoPath = request.Model.PhotoPath,
                VideoPath = request.Model.VideoPath,
                DayOfCreation = DateTime.Now,
            };

            await Unit.VideoRepository.AddVideo(video);
            await Unit.SaveAsync();

            return Success(new [] {model.PhotoPath, model.VideoPath});
        }
    }
}
