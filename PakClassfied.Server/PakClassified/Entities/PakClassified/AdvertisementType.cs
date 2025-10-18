using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakClassified.Entities.PakClassified
{
    public class AdvertisementType : BaseEntity<int>, INamedEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Advertisement> Advertisements { get; set; }
    }
}
