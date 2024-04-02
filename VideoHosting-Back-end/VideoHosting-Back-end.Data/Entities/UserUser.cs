namespace VideoHosting_Back_end.Data.Entities;
public class UserUser
{
    // User id that follows Subscripter
    public string SubscriberId { get; set; }

    // User that follows Subscripter
    public virtual User Subscriber { get; set; }

    // User id to be followed
    public string SubscripterId { get; set; }

    // User to be followed
    public virtual User Subscripter { get; set; }
}
