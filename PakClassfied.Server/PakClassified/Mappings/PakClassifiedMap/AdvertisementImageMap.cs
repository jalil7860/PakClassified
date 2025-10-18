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
    public class AdvertisementImageMap : IEntityTypeConfiguration<AdvertisementImage>
    {
        public void Configure(EntityTypeBuilder<AdvertisementImage> builder)
        {
            builder.HasKey(ai => ai.Id);

            builder.Property(ai => ai.Content).HasMaxLength(255).IsRequired();
            builder.Property(ai => ai.Caption).HasMaxLength(255);


            builder.HasOne(ai => ai.Advertisement)
                .WithMany(a => a.Images)
                .HasForeignKey(ai => ai.AdvertisementId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("AdvertisementImages", "dbo");
        }
    }
}
