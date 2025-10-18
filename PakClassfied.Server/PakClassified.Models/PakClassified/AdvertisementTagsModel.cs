using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakClassified.Models.PakClassified
{
    public class AdvertisementTagsModel : BaseDTO<int>
    {
        public string Name { get; set; }
        public int NoOfSearches { get; set; }
    }
}
