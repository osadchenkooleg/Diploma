using Hellang.Middleware.ProblemDetails;
using MediatR;
using System.Buffers.Text;
using VideoHosting.Api.Common;
using VideoHosting.Common.Responses;
using VideoHosting.Database.Abstraction;

namespace VideoHosting.Api.Application.Users.Commands;

public class UploadPhotoCommand : IRequest<Response<Unit>>
{
    public IFormFileCollection Files { get; }
    public string LoggedInUserId { get; }

    public UploadPhotoCommand(IFormFileCollection files, string loggedInUserId)
    {
        Files = files;
        LoggedInUserId = loggedInUserId;
    }

    public class Handler : BaseHandler<UploadPhotoCommand, Unit>
    {
        protected static string TargetDir => $@"{Directory.GetCurrentDirectory()}\wwwroot\images\";
        private readonly string[] _fileFormats = new[] {".JPG", ".png", ".jpg"};
        
        public Handler(IUnitOfWork unit) : base(unit)
        {
        }

        public override async Task<Response<Unit>> Handle(UploadPhotoCommand request, CancellationToken cancellationToken)
        {
            var file = request.Files.FirstOrDefault();
            if (file == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status404NotFound, "No files were uploaded");
            }

            var loggedInUser = request.LoggedInUserId;

            var user = await Unit.UserRepository.GetUserById(loggedInUser);
            if (user == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status404NotFound, "User not found");
            }

            
            var path = Path.Combine(Directory.GetCurrentDirectory(), "UsersContent\\UsersPhotos");
            
            if (!_fileFormats.Any(f => file.FileName.Contains(f)))
            {
                throw new ProblemDetailsException(StatusCodes.Status400BadRequest, "Image should be .jpg or .png");
            }
            
            var fileName = file.FileName.Contains(".JPG") || file.FileName.Contains(".jpg") ? loggedInUser + ".JPG" : loggedInUser + ".png";
            var fullPath = Path.Combine(path, fileName);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            
            var base64 = ResolveIFormFileToBase64(file).Split(',').Last();

            var wwwPath = CreatePath(fileName);
            var bytes = Convert.FromBase64String(base64.Split(',').Last());

            await using (var fs = new FileStream(wwwPath, FileMode.Create))
                await fs.WriteAsync(bytes, cancellationToken);

            await using var stream = File.Create(fullPath);
                
            await file.CopyToAsync(stream, cancellationToken);
                
            
            user.PhotoPath = fileName;

            await Unit.SaveAsync();

            return Success(new Unit());
        }

        private string CreatePath(string fileName) => $"{TargetDir}{fileName}";

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
