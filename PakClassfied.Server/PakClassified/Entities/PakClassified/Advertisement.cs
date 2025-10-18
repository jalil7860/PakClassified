using PakClassified.Entities.Location;
using PakClassified.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakClassified.Entities.PakClassified
{
    public class Advertisement : BaseEntity<int>, INamedEntity
    {
        public int SubCategoryId { get; set; }
        public int StatusId { get; set; }
        public int TypeId { get; set; }
        public int PostedById { get; set; }
        public int CityAreaId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Hits { get; set; }
        public string? Features { get; set; }
        public DateTime EndsOn { get; set; }
        public DateTime StartsOn { get; set; }
        public virtual ICollection<AdvertisementImage> Images { get; set; }
        public virtual ICollection<AdvertisementTag> Tags { get; set; }
        public string? Image { get; set; }
        public virtual CityArea CityArea { get; set; } = null!;
        [NotMapped]
        public City City { get; set; }
        public virtual AdvertisementStatus Status { get; set; } = null!;
        public virtual AdvertisementSubCategory SubCategory { get; set; } = null!;
        public virtual AdvertisementType Type { get; set; } = null!;
        public virtual User PostedBy { get; set; } = null!;

        public Advertisement()
        {
            Images = new List<AdvertisementImage>();
            Tags = new List<AdvertisementTag>();
        }
    }
}
