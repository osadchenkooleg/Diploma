using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoHosting.Data.Entities;

namespace VideoHosting.Database.Context.EntityConfiguration;

internal class UserDislikeConfiguration : IEntityTypeConfiguration<UserDislike>
{
    public void Configure(EntityTypeBuilder<UserDislike> builder)
    {
        builder
            .HasKey(x => new { x.UserId, x.VideoId });
        
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Dislikes)
            .HasForeignKey(x => x.UserId);

        builder
            .HasOne(x => x.Video)
            .WithMany(x => x.Dislikes)
            .HasForeignKey(x => x.VideoId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
