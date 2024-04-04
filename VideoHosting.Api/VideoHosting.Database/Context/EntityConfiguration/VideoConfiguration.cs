using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoHosting.Data.Entities;

namespace VideoHosting.Database.Context.EntityConfiguration;
internal class VideoConfiguration  : IEntityTypeConfiguration<Video>
{
    public void Configure(EntityTypeBuilder<Video> builder)
    {
        builder
            .HasMany(x => x.Commentaries)
            .WithOne(x => x.Video)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
