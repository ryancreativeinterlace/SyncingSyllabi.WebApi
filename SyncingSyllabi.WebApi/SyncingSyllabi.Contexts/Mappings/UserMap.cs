using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncingSyllabi.Contexts.Entities;
using SyncingSyllabi.Contexts.Mappings.Base;

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
            builder.Property(e => e.Password).HasColumnName("password");
            builder.Property(e => e.School).HasColumnName("school");
            builder.Property(e => e.Major).HasColumnName("major");
            builder.Property(e => e.ImageUrl).HasColumnName("image_url");
            builder.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
        }
    }
}
