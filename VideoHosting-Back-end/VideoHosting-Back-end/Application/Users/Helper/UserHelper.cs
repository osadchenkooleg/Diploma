using Hellang.Middleware.ProblemDetails;
using VideoHosting_Back_end.Application.Users.Models;
using VideoHosting_Back_end.Common.Constants;
using VideoHosting_Back_end.Common.Mappers;
using VideoHosting_Back_end.Database.Abstraction;

namespace VideoHosting_Back_end.Application.Users.Helper;

public static class UserHelper
{
    public static async Task<UserModel> GetUserByIdAsync(string id, string userId, IUnitOfWork unit)
    {
        var user = await unit.UserRepository.GetUserById(userId);
        if (user == null)
        {
            throw new ProblemDetailsException(StatusCodes.Status404NotFound, "Requested user not found");
        }
            
        var userSub = await unit.UserRepository.GetUserById(id);
        if (userSub == null)
        {
            throw new ProblemDetailsException(StatusCodes.Status404NotFound, "Logged in user not found");
        }

        var userDto = user.MapToUserModel();
        userDto.DoSubscribed = user.Subscribers.FirstOrDefault(x => x.Subscripter == userSub) != null;
        userDto.Admin = await unit.UserManager.IsInRoleAsync(userSub, "Admin");
        userDto.PhotoPath = unit.AppSwitchRepository.GetValue(AppSwitchConstants.UserPhotoKey) + userDto.PhotoPath;
            
        return userDto;
    }
}
