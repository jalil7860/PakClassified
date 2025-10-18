using PakClassified.Entities.PakClassified;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace PakClassified.Entities.Location
{
    public class CityArea : BaseEntity<int>, INamedEntity
    {
        public string Name { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<Advertisement> Advertisements { get; set; }
    }
}