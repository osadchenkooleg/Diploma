namespace VideoHosting_Back_end.Data.Entities;
public class VideoUser
{
    public Guid VideoId { get; set; }

    public virtual Video Video { get; set; }

    public string UserId { get; set; }

    public virtual User User { get; set; }
}
