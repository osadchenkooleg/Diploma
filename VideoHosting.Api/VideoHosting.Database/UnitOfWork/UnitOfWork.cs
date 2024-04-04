using Microsoft.AspNetCore.Identity;
using VideoHosting.Data.Entities;
using VideoHosting.Database.Abstraction;
using VideoHosting.Database.Context;

namespace VideoHosting.Database.UnitOfWork;
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationContext _context;

    public UnitOfWork(UserManager<User> userManager, RoleManager<Role> roleManager, ApplicationContext dbContext,
        IUserRepository userRepository, IVideoRepository videoRepository, ICommentaryRepository commentaryRepository,
        IAppSwitchRepository appSwitchRepository)
    {
        UserManager = userManager;
        _context = dbContext;
        RoleManager = roleManager;
        UserRepository = userRepository;
        VideoRepository = videoRepository;
        CommentaryRepository = commentaryRepository;
        AppSwitchRepository = appSwitchRepository;
    }

    public IUserRepository UserRepository { get; }

    public IVideoRepository VideoRepository { get; }

    public ICommentaryRepository CommentaryRepository { get; }
        
    public IAppSwitchRepository AppSwitchRepository { get; }

    public UserManager<User> UserManager { get; }

    public RoleManager<Role> RoleManager { get; }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
