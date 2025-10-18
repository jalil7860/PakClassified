using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakClassified.Models.Location
{
    public class ProvinceModel : BaseDTO<int>
    {
        public string Name { get; set; }
        public int CountryId { get; set; }
        public CountryModel Country { get; set; }
    }
}
