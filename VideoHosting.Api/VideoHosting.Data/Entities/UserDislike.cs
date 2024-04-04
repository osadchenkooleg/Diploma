namespace VideoHosting.Data.Entities;
public class UserDislike
{
    public Guid VideoId { get; set; }

    public virtual Video Video { get; set; }
    
    public string UserId { get; set; }

    public virtual User User { get; set; }
}
