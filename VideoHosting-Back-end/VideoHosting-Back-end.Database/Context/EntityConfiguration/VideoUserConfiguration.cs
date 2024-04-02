using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoHosting_Back_end.Data.Entities;

namespace VideoHosting_Back_end.Database.Context.EntityConfiguration;

internal class VideoUserConfiguration : IEntityTypeConfiguration<VideoUser>
{
    public void Configure(EntityTypeBuilder<VideoUser> builder)
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
