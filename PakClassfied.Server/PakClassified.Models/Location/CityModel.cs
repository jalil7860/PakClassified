using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakClassified.Models.Location
{
    public class CityModel : BaseDTO<int>
    {
        public string Name { get; set; }
        public ProvinceModel Province { get; set; }
    }
}
