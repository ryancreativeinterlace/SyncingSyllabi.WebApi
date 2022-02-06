using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncingSyllabi.Contexts.Entities;

namespace SyncingSyllabi.Contexts.Mappings
{
    public class UserMap : BaseTrackedEntityMap<UserEntity>
    {
        public UserMap()
        {

        }
        public override void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("users");

            builder.Property(e => e.FirstName).HasColumnName("first_name");
            builder.Property(e => e.LastName).HasColumnName("last_name");
            builder.Property(e => e.Email).HasColumnName("email");
        }
    }
}
