using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VideoHosting_Back_end.Data.Entities;

namespace VideoHosting_Back_end.Database.Context.EntityConfiguration;
internal class UserUserConfiguration : IEntityTypeConfiguration<UserUser>
{
    public void Configure(EntityTypeBuilder<UserUser> builder)
    {
        builder
            .HasKey(x => new { x.SubscriberId, x.SubscripterId });
    }
}

