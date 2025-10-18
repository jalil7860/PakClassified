using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakClassified.Entities.PakClassified
{
    public class AdvertisementCategory : BaseEntity<int>, INamedEntity
    {
        public string Name { get; set; }
        [AllowNull]
        public string Description { get; set; }
        public string? Image { get; set; }  
        public virtual ICollection<AdvertisementSubCategory> SubCategories { get; set; }

    }
}
