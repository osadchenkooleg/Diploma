using VideoHosting.Data.Entities;

namespace VideoHosting.Api.Application.Auth.Models;

public class UserGetModel
{
    public UserGetModel(User user)
    {
        Id = user.Id;
        UserName = user.UserName;
        Email = user.Email;
    }

    public string Id { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }
}
