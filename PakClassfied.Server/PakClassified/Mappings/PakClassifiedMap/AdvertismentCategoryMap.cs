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
    public class AdvertismentCategoryMap : IEntityTypeConfiguration<AdvertisementCategory>
    {
        public void Configure(EntityTypeBuilder<AdvertisementCategory> builder)
        {
            builder.HasKey(ac => ac.Id);
            builder.Property(ac => ac.Name).HasMaxLength(100).IsRequired();
            builder.Property(ac => ac.Image).HasMaxLength(10000);

            builder.HasMany(ac => ac.SubCategories)
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("AdvertisementCategories", "dbo");
        }
    }
}
