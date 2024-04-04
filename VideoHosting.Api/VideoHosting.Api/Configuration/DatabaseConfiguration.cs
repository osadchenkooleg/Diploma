using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VideoHosting.Common.Options;
using VideoHosting.Data.Entities;
using VideoHosting.Database.Abstraction;
using VideoHosting.Database.Context;
using VideoHosting.Database.Repositories;
using VideoHosting.Database.UnitOfWork;

namespace VideoHosting.Api.Configuration;

public static class DatabaseConfiguration
{
    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStrings = new ConnectionStringOptions(configuration);

        services.AddDbContext(connectionStrings.DefaultConnection);
        services.ConfigureIdentity();

        services.AddTransient<UserManager<User>>();
        services.AddTransient<RoleManager<Role>>();

        services.ConfigureRepositories();
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
    {
        return services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(connectionString));
    }

    private static void ConfigureIdentity(this IServiceCollection services)
    {
        var builder = services.AddIdentityCore<User>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 8;
            options.User.RequireUniqueEmail = true;
        });

        builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);

        builder.AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();
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
