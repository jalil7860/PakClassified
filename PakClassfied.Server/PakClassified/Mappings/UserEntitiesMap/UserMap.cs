using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PakClassified.Entities.UserEntities;
using PakClassified.Entities.PakClassified;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakClassified.Mappings.UserEntitiesMap
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(ac => ac.Name).HasMaxLength(100).IsRequired();
            builder.Property(ac => ac.Password).HasMaxLength(100).IsRequired();
            builder.Property(ac => ac.Email).HasMaxLength(200).IsRequired();
            builder.Property(ac => ac.ContactNumber).HasMaxLength(20);
            builder.Property(ac => ac.SecurityQuestion).HasMaxLength(300);
            builder.Property(ac => ac.SecurityAnswer).HasMaxLength(300);


            builder.HasMany(x => x.Advertisements)
                .WithOne(a => a.PostedBy)
                .HasForeignKey(a => a.PostedById)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Users", "dbo");
        }

    }
}
