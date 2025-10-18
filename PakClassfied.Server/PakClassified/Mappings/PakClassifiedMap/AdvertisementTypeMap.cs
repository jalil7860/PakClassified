using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PakClassified.Entities.PakClassified;

namespace PakClassified.Mappings.PakClassifiedMap
{
    internal class AdvertisementTypeMap
    {
        public void Configure(EntityTypeBuilder<AdvertisementType> builder)
        {
            builder.HasKey(at => at.Id);
            builder.Property(at => at.Name).HasMaxLength(100).IsRequired();

            builder.HasMany(at => at.Advertisements)
                .WithOne(a => a.Type)
                .HasForeignKey(a => a.TypeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("AdvertisementTypes", "dbo");
        }
    }
}
