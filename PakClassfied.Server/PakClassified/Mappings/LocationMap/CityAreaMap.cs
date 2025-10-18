using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PakClassified.Entities.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakClassified.Mappings.LocationMap
{
    public class CityAreaMap : IEntityTypeConfiguration<CityArea>
    {
        public void Configure(EntityTypeBuilder<CityArea> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(ca => ca.Name).HasMaxLength(100).IsRequired();

            builder.HasMany(ca => ca.Advertisements)
                .WithOne(a => a.CityArea)
                .HasForeignKey(a => a.CityAreaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ca => ca.City)
                .WithMany(c => c.CityAreas)
                .HasForeignKey(ca =>  ca.CityId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("CityAreas", "dbo");

        }
    }
}
