using Microsoft.AspNetCore.Identity;
using VideoHosting.Data.Entities;

namespace VideoHosting.Database.Abstraction;
public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }

    IVideoRepository VideoRepository { get; }

    ICommentaryRepository CommentaryRepository { get; }

    IAppSwitchRepository AppSwitchRepository { get; }

    UserManager<User> UserManager { get; }

    RoleManager<Role> RoleManager { get; }

    Task<int> SaveAsync();
}
