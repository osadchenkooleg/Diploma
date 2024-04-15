using VideoHosting.Api.Application.Credentials.Models;
using VideoHosting.Data.Entities;
using VideoHosting_Back_end.Application.Users.Models;

namespace VideoHosting.Api.Common;
public static class UserMapper
{
    public static UserLoginGetModel MapToUserLoginGetModel(this User user)
    {
        return new UserLoginGetModel
        {
            Id = user.Id,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber
        };
    }

    public static UserModel MapToUserModel(this User user)
    {
        return new UserModel
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email ?? string.Empty,
            Surname = user.Surname,
            Faculty = user.Faculty,
            Group = user.Group,
            PhotoPath = user.PhotoPath,
            DateOfCreation = user.DateOfCreation,
            Sex = user.Sex,
            Subscribers = user.Subscribers.Count,
            Subscriptions = user.Subscriptions.Count,
        };
    }

    public static User MapToUser(this UserModel user)
    {
        return new User
        {
            Name = user.Name,
            Surname = user.Surname,
            Faculty = user.Faculty,
            Group = user.Group,
            PhotoPath = user.PhotoPath,
            DateOfCreation = user.DateOfCreation,
            Sex = user.Sex,
            Subscribers = new List<UserUser>(),
            Subscriptions = new List<UserUser>(),
        };
    }
}
