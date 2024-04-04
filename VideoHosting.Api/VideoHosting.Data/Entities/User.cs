using Microsoft.AspNetCore.Identity;

namespace VideoHosting.Data.Entities;

public class User : IdentityUser
{
    public string Name { get; set; }

    public string Surname { get; set; }

    public bool Sex { get; set; }

    public string Faculty { get; set; }

    public string Group { get; set; }

    public int? TempPassword { get; set; }

    public DateTime DateOfCreation { get; set; }

    public string? PhotoPath { get; set; }

    public virtual List<Video> Videos { get; set; } = new();

    public virtual List<Commentary> Commentaries { get; set; } = new();

    public virtual List<UserUser> Subscribers { get; set; } = new();

    public virtual List<UserUser> Subscriptions { get; set; } = new();

    public virtual List<UserLike> Likes { get; set; } = new();

    public virtual List<UserDislike> Dislikes { get; set; } = new();

    public void AddLike(Video video)
    {
        var dislike = Dislikes.FirstOrDefault(x => x.Video == video);
        if (dislike != null)
        {
            Dislikes.Remove(dislike);
        }

        Likes.Add(new UserLike() { Video = video, User = this });
    }

    public void DeleteLike(Video video)
    {
        var like = Likes.FirstOrDefault(x => x.Video == video);
        if (like != null)
        {
            Likes.Remove(like);
        }
    }

    public void AddDislike(Video video)
    {
        var like = Likes.FirstOrDefault(x => x.Video == video);
        if (like != null)
        {
            Likes.Remove(like);
        }

        Dislikes.Add(new UserDislike() { Video = video, User = this });
    }

    public void DeleteDislike(Video video)
    {
        var like = Dislikes.FirstOrDefault(x => x.Video == video);
        if (like != null)
        {
            Dislikes.Remove(like);
        }
    }

    public void Subscribe(User user)
    {
        if (Subscriptions.FirstOrDefault(x => x.Subscripter == user) == null)
        {
            Subscriptions.Add(new UserUser { Subscriber = this, Subscripter = user });
        }
    }

    public void Unsubscribe(User user)
    {
        var sub = Subscriptions.FirstOrDefault(x => x.Subscripter == user);
        if (sub != null)
        {
            Subscribers.Remove(sub);
        }
    }
}
