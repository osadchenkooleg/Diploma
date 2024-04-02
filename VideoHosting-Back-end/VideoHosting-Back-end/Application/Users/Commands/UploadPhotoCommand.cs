using Hellang.Middleware.ProblemDetails;
using MediatR;
using VideoHosting_Back_end.Common.Common;
using VideoHosting_Back_end.Data.Entities;
using VideoHosting_Back_end.Database.Abstraction;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace VideoHosting_Back_end.Application.Users.Commands;

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
        private readonly string[] _fileFormats = new[] {".JPG", ".png", ".jpg"};
        
        public Handler(IUnitOfWork unit) : base(unit)
        {
        }

        public override async Task<Response<Unit>> Handle(UploadPhotoCommand request, CancellationToken cancellationToken)
        {
            var files = request.Files;
            var loggedInUser = request.LoggedInUserId;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "UsersContent/UsersPhotos");
            
            if (!_fileFormats.Any(f => files[0].FileName.Contains(f)))
            {
                throw new ProblemDetailsException(StatusCodes.Status400BadRequest, "Image should be .jpg or .png");
            }
            
            var storePath = files[0].FileName.Contains(".JPG") || files[0].FileName.Contains(".jpg") ? loggedInUser + ".JPG" : loggedInUser + ".png";
            var fullPath = Path.Combine(path, storePath);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            await using var stream = File.Create(fullPath);
                
            await files[0].CopyToAsync(stream, cancellationToken);
                
            var user = await Unit.UserRepository.GetUserById(loggedInUser);
            if (user == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status404NotFound, "User not found");
            }
            user.PhotoPath = storePath;

            await Unit.SaveAsync();

            return Success(new Unit());
        }
    }
}
