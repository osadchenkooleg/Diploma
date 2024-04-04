using VideoHosting.Api.Application.Commentaries.Models;
using VideoHosting.Data.Entities;

namespace VideoHosting.Api.Application.Commentaries.Mappers;

public static class CommentaryMapper
{
    public static CommentaryModel MapToCommentaryModel(this Commentary model)
    {
        return new CommentaryModel
        {
            Id = model.Id,
            UserId = model.User.Id,
            Content = model.Content,
            DayOfCreation = model.DayOfCreation,
            VideoId = model.Video.Id,
        };
    }
}
