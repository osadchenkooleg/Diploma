using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoHosting.Data.Entities;

namespace VideoHosting.Database.Context.EntityConfiguration;
internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasMany(x => x.Videos)
            .WithOne(x => x.User);

        builder
            .HasMany(x => x.Commentaries)
            .WithOne(x => x.User);

        builder
            .HasMany(x => x.Subscribers)
            .WithOne(x => x.Subscripter)
            .HasForeignKey(x => x.SubscripterId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(x => x.Subscriptions)
            .WithOne(x => x.Subscriber)
            .HasForeignKey(x => x.SubscriberId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
