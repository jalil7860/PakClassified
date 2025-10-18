using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PakClassified.Entities.PakClassified;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakClassified.Mappings.PakClassifiedMap
{
    public class AdvertisementSubCategoryMap : IEntityTypeConfiguration<AdvertisementSubCategory>
    {
        public void Configure(EntityTypeBuilder<AdvertisementSubCategory> builder)
        {
            builder.HasKey(asc => asc.Id);
            builder.Property(asc => asc.Name).HasMaxLength(100).IsRequired();
            builder.Property(asc => asc.Image).HasMaxLength(10000);

            builder.HasMany(asc => asc.Advertisements)
                .WithOne(a => a.SubCategory)
                .HasForeignKey(a => a.SubCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(asc => asc.Category)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(asc => asc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("AdvertisementSubCategories", "dbo");

        }
    }
}
