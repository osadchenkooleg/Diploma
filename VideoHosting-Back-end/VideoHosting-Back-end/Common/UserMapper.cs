using VideoHosting_Back_end.Application.Auth.Models;
using VideoHosting_Back_end.Application.Credentials.Models;
using VideoHosting_Back_end.Application.Users.Models;
using VideoHosting_Back_end.Data.Entities;

namespace VideoHosting_Back_end.Common.Mappers;
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

    public static User MapToUser(this UserRegistrationModel user)
    {
        return new User
        {
            Name = user.Name,
            Surname = user.Surname,
            Faculty = user.Faculty,
            Group = user.Group,
            Sex = user.Sex,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber
        };
    }
    
    public static User MapToUser(this UserLoginModel user)
    {
        return new User
        {
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
        };
    }

    public static UserModel MapToUserModel(this UserRegistrationModel user)
    {
        return new UserModel
        {
            Name = user.Name,
            Surname = user.Surname,
            Faculty = user.Faculty,
            Group = user.Group,
            Sex = user.Sex,
        };
    }

    public static UserLoginModel MapToUserLoginModel(this UserRegistrationModel user)
    {
        return new UserLoginModel
        {
            Email = user.Email,
            Password = user.Password,
            PhoneNumber = user.PhoneNumber
        };
    }
}
