using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoHosting.Data.Entities;

namespace VideoHosting.Database.Context.EntityConfiguration;
internal class UserUserConfiguration : IEntityTypeConfiguration<UserUser>
{
    public void Configure(EntityTypeBuilder<UserUser> builder)
    {
        builder
            .HasKey(x => new { x.SubscriberId, x.SubscripterId });
    }
}

