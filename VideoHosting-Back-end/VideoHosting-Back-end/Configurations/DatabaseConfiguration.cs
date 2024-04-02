using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VideoHosting_Back_end.Common.Configuration;
using VideoHosting_Back_end.Data.Entities;
using VideoHosting_Back_end.Database.Abstraction;
using VideoHosting_Back_end.Database.Context;
using VideoHosting_Back_end.Database.Repositories;
using VideoHosting_Back_end.Database.UnitOfWork;

namespace VideoHosting_Back_end.Configurations;

public static class DatabaseConfiguration
{
    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStrings = new ConnectionStringOptions(configuration);

        services.AddDbContext(connectionStrings.DefaultConnection);
        services.ConfigureIdentity();
        services.ConfigureRepositories();

        services.AddTransient<UserManager<User>>();
        services.AddTransient<SignInManager<User>>();
        services.AddTransient<RoleManager<UserRole>>();
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
    {
        return services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(connectionString));
    }

    private static void ConfigureIdentity(this IServiceCollection services)
    {
        var builder = services.AddIdentity<User, UserRole>(opts =>
            {
                opts.Password.RequiredLength = 5;   // minimum length
                opts.Password.RequireNonAlphanumeric = false;   // require non alphabetic symbols
                opts.Password.RequireLowercase = false; // require lowercase  symbols
                opts.Password.RequireUppercase = false; // require uppercase symbols
                opts.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<ApplicationContext>();
    }
    private static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<ICommentaryRepository, CommentaryRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IVideoRepository, VideoRepository>();
        services.AddScoped<IAppSwitchRepository, AppSwitchRepository>();
    }
}
