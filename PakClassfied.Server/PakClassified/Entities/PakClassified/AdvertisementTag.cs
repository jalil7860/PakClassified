using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakClassified.Entities.PakClassified
{
    public class AdvertisementTag : BaseEntity<int>, INamedEntity
    {
        public string Name { get; set; }
        [AllowNull]
        public int NoOfSearches { get; set; }

        public virtual ICollection<Advertisement> Advertisements { get; set; }
    }
}
