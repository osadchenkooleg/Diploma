using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoHosting.Data.Entities;

namespace VideoHosting.Database.Context.EntityConfiguration;

internal class CommentaryConfiguration : IEntityTypeConfiguration<Commentary>
{
    public void Configure(EntityTypeBuilder<Commentary> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Commentaries)
            .HasForeignKey(x => x.UserId);

        builder
            .HasOne(x => x.Video)
            .WithMany(x => x.Commentaries)
            .HasForeignKey(x => x.VideoId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
