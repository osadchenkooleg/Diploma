using System.ComponentModel.DataAnnotations;

namespace VideoHosting.Api.Application.Commentaries.Models;

public class CommentaryApplyModel
{
    [Required]
    public string UserId { get; set; }

    [Required]
    public string Content { get; set; }
    
    [Required]
    public Guid VideoId { get; set; }
}
