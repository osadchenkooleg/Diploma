using Microsoft.AspNetCore.Identity;
using VideoHosting.Data.Entities;
using VideoHosting.Database.Context;

namespace VideoHosting.Api.Extension;

public static class DatabaseSeederExtension
{
    public static IApplicationBuilder UseItToSeedSqlServer(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app, nameof(app));

        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<Role>>();
            var context = services.GetRequiredService<ApplicationContext>();
            //DatabaseSeeder.SeedAsync(userManager, roleManager, context).GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {

        }

        return app;
    }
}
