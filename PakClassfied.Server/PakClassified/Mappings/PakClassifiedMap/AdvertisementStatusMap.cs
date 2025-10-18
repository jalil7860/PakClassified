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
    public class AdvertisementStatusMap : IEntityTypeConfiguration<AdvertisementStatus>
    {
        public void Configure(EntityTypeBuilder<AdvertisementStatus> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name).HasMaxLength(100).IsRequired();

            builder.HasMany(s => s.Advertisements)
                .WithOne(a => a.Status)
                .HasForeignKey(a => a.StatusId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("AdvertisementStatuses", "dbo");
        }
    }
}
