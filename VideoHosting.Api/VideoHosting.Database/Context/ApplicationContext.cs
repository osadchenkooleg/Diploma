using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VideoHosting.Data.Entities;
using VideoHosting.Database.Context.EntityConfiguration;

namespace VideoHosting.Database.Context;
public class ApplicationContext : IdentityDbContext<User, Role, string>
{
    public virtual DbSet<Video> Videos { get; set; }

    public virtual DbSet<UserDislike> Likes { get; set; }

    public virtual DbSet<UserDislike> Dislikes { get; set; }

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
            .ApplyConfiguration(new UserLikeConfiguration())
            .ApplyConfiguration(new UserDislikeConfiguration())
            .ApplyConfiguration(new CommentaryConfiguration())
            .ApplyConfiguration(new RolesConfiguration());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLazyLoadingProxies();
    }
}
