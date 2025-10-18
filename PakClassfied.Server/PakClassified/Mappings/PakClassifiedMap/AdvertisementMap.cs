using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PakClassified.Entities.PakClassified;
using PakClassified.Entities.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakClassified.Mappings.PakClassifiedMap
{
    public class AdvertisementMap : IEntityTypeConfiguration<Advertisement>
    {
        public void Configure(EntityTypeBuilder<Advertisement> builder)
        {
            builder.Property(a => a.Name).HasMaxLength(250).IsRequired();
            builder.Property(a => a.Price).IsRequired();
            builder.Property(a => a.Description).HasMaxLength(500);
            builder.Property(a => a.Image).HasMaxLength(10000);
            builder.Property(a => a.Features).HasMaxLength(500);
            
            builder.HasOne(a => a.SubCategory)
                .WithMany(sc => sc.Advertisements)
                .HasForeignKey(a => a.SubCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a=> a.Status)
                .WithMany(s => s.Advertisements)
                .HasForeignKey(a => a.StatusId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.Type)
                .WithMany(t => t.Advertisements)
                .HasForeignKey(a => a.TypeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.CityArea)
                .WithMany(ca => ca.Advertisements)
                .HasForeignKey(a => a.CityAreaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne( a => a.PostedBy)
                .WithMany( u => u.Advertisements)
                .HasForeignKey(a => a.PostedById)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.Images)
                .WithOne(im => im.Advertisement)
                .HasForeignKey( im => im.AdvertisementId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.Tags)
                .WithMany(t => t.Advertisements);

            builder.ToTable("Advertisements", "dbo");

        }
    }
}
