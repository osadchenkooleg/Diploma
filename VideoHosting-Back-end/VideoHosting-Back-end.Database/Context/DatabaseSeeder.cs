using Microsoft.AspNetCore.Identity;
using VideoHosting_Back_end.Data.Entities;

namespace VideoHosting_Back_end.Database.Context;
public class DatabaseSeeder
{
    public static async Task SeedAsync(UserManager<User> userManager, RoleManager<UserRole> roleManager, ApplicationContext context)
        {
            var adminRole = new UserRole { Name = "Admin", Description = "Can delete video, users, commentaries" };
            var userRole = new UserRole { Name = "User", Description = "Can add video, commentaries, likes" };
            var globalAdmin = new UserRole { Name = "GlobalAdmin", Description = "Can do everything that admin and can add,delete them" };

            await roleManager.CreateAsync(userRole);
            await roleManager.CreateAsync(adminRole);
            await roleManager.CreateAsync(globalAdmin);

            await context.SaveChangesAsync();


            var user1 = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = "osasdchenko.adnrei@gmail.com",
                UserName = "AdnreiOsadchenko",
                PhoneNumber = "380636206333",
                Name = "Adnrei",
                Surname = "Osadchenko",
                DateOfCreation = DateTime.Now,
                PhotoPath = "",
                Videos = new(),
                Commentaries = new(),
                Subscribers = new(),
                Subscriptions = new(),
                Likes = new(),
                Dislikes = new(),
                TempPassword = 0,
                Sex = true,
                Faculty = "",
                Group = ""
            };

            await userManager.CreateAsync(user1, "Osadchenko123");
            user1.UserName = user1.Id;
            await userManager.UpdateAsync(user1);

            var user2 = new User { 
                Id = Guid.NewGuid().ToString(),
                Email = "osasdchenko.max@gmail.com", 
                UserName = "MaxOsadchenko", 
                PhoneNumber = "380636206332",
                Name = "Max",
                Surname = "Osadchenko",
                DateOfCreation = DateTime.Now,
                TempPassword = 0,
                PhotoPath = "",
                Videos = new(),
                Commentaries = new(),
                Subscribers = new(),
                Subscriptions = new(),
                Likes = new(),
                Dislikes = new(),
                Sex = true,
                Faculty = "",
                Group = ""
            };
            
            await userManager.CreateAsync(user2, "Osadchenko123");
            user2.UserName = user2.Id;
            await userManager.UpdateAsync(user2);

            User admin = new User 
            { 
                Id = Guid.NewGuid().ToString(),
                Email = "oleg.osadchenko.v@gmail.com", 
                UserName = "OlegOsadchenko", 
                PhoneNumber = "380682819737",
                Name = "Oleg",
                Surname = "Osadchenko",
                DateOfCreation = DateTime.Now,
                PhotoPath = "",
                Videos = new(),
                Commentaries = new(),
                Subscribers = new(),
                Subscriptions = new(),
                Likes = new(),
                Dislikes = new(),
                TempPassword = 0,
                Sex = true,
                Faculty = "",
                Group = ""
            };
            
            await userManager.CreateAsync(admin, "Osadchenko123");
            admin.UserName = admin.Id;
            await userManager.UpdateAsync(admin);
        
            await userManager.AddToRoleAsync(admin, adminRole.Name);
            await userManager.AddToRoleAsync(admin, userRole.Name);
            await userManager.AddToRoleAsync(admin, globalAdmin.Name);

            await userManager.AddToRoleAsync(user1, userRole.Name);
            await userManager.AddToRoleAsync(user2, userRole.Name);

            await context.SaveChangesAsync();
        }
}
