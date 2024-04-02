using System.ComponentModel.DataAnnotations;

namespace VideoHosting_Back_end.Application.Commentaries.Models;

public class CommentaryApplyModel
{
    [Required]
    public string UserId { get; set; }

    [Required]
    public string Content { get; set; }

    public DateTime DayOfCreation { get; set; }

    [Required]
    public Guid VideoId { get; set; }
}
