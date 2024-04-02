using Microsoft.AspNetCore.Identity;
using VideoHosting_Back_end.Data.Entities;
using VideoHosting_Back_end.Database.Context;

namespace VideoHosting_Back_end.Extension;

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
            var roleManager = services.GetRequiredService<RoleManager<UserRole>>();
            var context = services.GetRequiredService<ApplicationContext>();
            DatabaseSeeder.SeedAsync(userManager, roleManager, context).GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {

        }

        return app;
    }
}
