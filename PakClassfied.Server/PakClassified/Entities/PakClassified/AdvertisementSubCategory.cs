using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakClassified.Entities.PakClassified
{
    public class AdvertisementSubCategory : BaseEntity<int>, INamedEntity
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public virtual AdvertisementCategory Category { get; set; }
        public virtual ICollection<Advertisement> Advertisements { get; set; }
    }
}
