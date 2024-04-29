using Hellang.Middleware.ProblemDetails;
using MediatR;
using VideoHosting.Api.Application.Commentaries.Mappers;
using VideoHosting.Api.Application.Commentaries.Models;
using VideoHosting.Api.Common;
using VideoHosting.Common.Responses;
using VideoHosting.Data.Entities;
using VideoHosting.Database.Abstraction;

namespace VideoHosting.Api.Application.Commentaries.Commands;

public class CreateCommentaryCommand : IRequest<Response<CommentaryModel>>
{
    public CommentaryApplyModel Model { get; }

    public CreateCommentaryCommand(CommentaryApplyModel model)
    {
        Model = model;
    }

    public class Handler : BaseHandler<CreateCommentaryCommand, CommentaryModel>
    {
        public Handler(IUnitOfWork unit) : base(unit)
        {
        }

        public override async Task<Response<CommentaryModel>> Handle(CreateCommentaryCommand request, CancellationToken cancellationToken)
        {

            var user = await Unit.UserRepository.GetUserById(request.Model.UserId);
            if (user == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status404NotFound, "User not found");
            }
            
            var video = await Unit.VideoRepository.GetVideoById(request.Model.VideoId);
            if (video == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status404NotFound, "Video not found");
            }

            var commentary = new Commentary
            {
                Content = request.Model.Content,
                User = user,
                Video = video,
                DayOfCreation = DateTime.Now
            };
            Unit.CommentaryRepository.AddCommentary(commentary);
            await Unit.SaveAsync();

            return Success(commentary.MapToCommentaryModel());
        }
    }
}
