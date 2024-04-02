using Microsoft.AspNetCore.Identity;
using VideoHosting_Back_end.Data.Entities;

namespace VideoHosting_Back_end.Database.Abstraction;
public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }

    IVideoRepository VideoRepository { get; }

    ICommentaryRepository CommentaryRepository { get; }

    IAppSwitchRepository AppSwitchRepository { get; }

    UserManager<User> UserManager { get; }

    RoleManager<UserRole> RoleManager { get; }

    Task<int> SaveAsync();
}
