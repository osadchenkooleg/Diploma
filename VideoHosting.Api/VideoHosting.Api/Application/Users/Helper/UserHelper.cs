using Hellang.Middleware.ProblemDetails;
using VideoHosting.Api.Common;
using VideoHosting.Common.Constants;
using VideoHosting.Database.Abstraction;
using VideoHosting_Back_end.Application.Users.Models;

namespace VideoHosting.Api.Application.Users.Helper;

public static class UserHelper
{
    public static async Task<UserModel> GetUserByIdAsync(string userId, string userThatGets, IUnitOfWork unit)
    {
        var user = await unit.UserRepository.GetUserById(userId);
        if (user == null)
        {
            throw new ProblemDetailsException(StatusCodes.Status404NotFound, "Requested user not found");
        }
            
        var loggedInUser = await unit.UserRepository.GetUserById(userThatGets);
        if (loggedInUser == null)
        {
            throw new ProblemDetailsException(StatusCodes.Status404NotFound, "Logged in user not found");
        }

        var userDto = user.MapToUserModel();
        userDto.DoSubscribed = user.Subscribers.FirstOrDefault(x => x.Subscriber == loggedInUser) != null;
        userDto.Admin = await unit.UserManager.IsInRoleAsync(loggedInUser, "Admin");
        userDto.PhotoPath = unit.AppSwitchRepository.GetValue(AppSwitchConstants.UserPhotoKey) + userDto.PhotoPath;
            
        return userDto;
    }
}
