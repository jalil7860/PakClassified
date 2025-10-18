using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakClassified.Entities.Location
{
    public class City : BaseEntity<int>, INamedEntity
    {
        public string Name { get; set; }

        public int ProvinceId { get; set; }
        public virtual Province Province { get; set; }
        public virtual ICollection<CityArea> CityAreas { get; set; }

        [NotMapped]
        public Country Country
        {
            get
            {
                return Province.Country;
            }
            set
            {
                Province.Country = value;
            }
        }


    }
}
