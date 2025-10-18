using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakClassified.Entities.Location
{
    public class Country : BaseEntity<int>, INamedEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Province> Provinces { get; set; }
    }
}
