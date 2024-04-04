using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoHosting.Data.Entities;

namespace VideoHosting.Database.Context.EntityConfiguration;

internal class UserLikeConfiguration : IEntityTypeConfiguration<UserLike>
{
    public void Configure(EntityTypeBuilder<UserLike> builder)
    {
        builder
            .HasKey(x => new { x.UserId, x.VideoId });
        
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Likes)
            .HasForeignKey(x => x.UserId);

        builder
            .HasOne(x => x.Video)
            .WithMany(x => x.Likes)
            .HasForeignKey(x => x.VideoId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
