using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoHosting.Data.Entities;

namespace VideoHosting.Database.Context.EntityConfiguration;
internal class RolesConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Name).IsUnique();

        var roles = new List<Role>()
        {
            new()
            {
                Id = "Admin",
                Name = "Admin",
                NormalizedName = "Admin"
            },
            new()
            {
                Id = "User",
                Name = "User",
                NormalizedName = "User"
            }
        };
        builder.HasData(roles);
    }
}
