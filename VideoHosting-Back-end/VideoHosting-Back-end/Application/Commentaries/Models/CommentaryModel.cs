using System.ComponentModel.DataAnnotations;

namespace VideoHosting_Back_end.Application.Commentaries.Models;

public class CommentaryModel
{
    public Guid Id { get; set; }
    
    public string UserId { get; set; }
    
    public string Content { get; set; }

    public DateTime DayOfCreation { get; set; }
    
    public Guid VideoId { get; set; }
}
