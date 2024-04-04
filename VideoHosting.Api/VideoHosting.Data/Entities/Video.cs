namespace VideoHosting.Data.Entities;
public class Video
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
        
    public DateTime DayOfCreation { get; set; }

    public int Views { get; set; }

    public virtual User User { get; set; }

    public virtual List<Commentary> Commentaries { get; set; } = new();

    public virtual List<UserLike> Likes { get; set; } = new();

    public virtual List<UserDislike> Dislikes { get; set; } = new();

    public string PhotoPath { get; set; }

    public string VideoPath { get; set; }
    
    public void AddCommentary(Commentary commentary)
    {
        Commentaries.Add(commentary);
    }
    public void DeleteCommentary(Commentary commentary)
    {
        Commentaries.Remove(commentary);
    }
}
