using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakClassified.Models.PakClassified
{
    public class AdvertisementCategoryModel  : BaseDTO<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
    }
}
