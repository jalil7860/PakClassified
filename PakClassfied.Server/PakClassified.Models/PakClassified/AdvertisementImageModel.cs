using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakClassified.Models.PakClassified
{
    public class AdvertisementImageModel : BaseDTO<int>
    {
        public int Rank { get; set; }
        public string? Caption { get; set; }
        public string Content { get; set; }
        public AdvertisementModel Advertisement { get; set; }
        public int AdvertisementId { get; set; }
    }
}
