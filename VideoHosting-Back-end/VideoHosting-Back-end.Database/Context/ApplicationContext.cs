using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VideoHosting_Back_end.Data.Entities;
using VideoHosting_Back_end.Data.Infrastructure;
using VideoHosting_Back_end.Database.Context.EntityConfiguration;

namespace VideoHosting_Back_end.Database.Context;
public class ApplicationContext : IdentityDbContext<User>
{
    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<Video> Videos { get; set; }

    public virtual DbSet<Commentary> Commentaries { get; set; }

    public virtual DbSet<AppSwitch> AppSwitches { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder
            .ApplyConfiguration(new UserConfiguration())
            .ApplyConfiguration(new UserUserConfiguration())
            .ApplyConfiguration(new VideoUserConfiguration())
            .ApplyConfiguration(new VideoConfiguration());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLazyLoadingProxies();
    }
}
