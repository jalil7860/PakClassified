using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakClassified.Models.Location
{
    public class CityAreaModel : BaseDTO<int>
    {
        public string Name { get; set; }
        //public int CityId { get; set; }
        public CityModel City { get; set; }
    }
}
