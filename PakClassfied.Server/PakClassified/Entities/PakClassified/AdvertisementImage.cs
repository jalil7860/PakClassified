using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakClassified.Entities.PakClassified
{
    public class AdvertisementImage : BaseEntity<int>
    {
        [AllowNull]
        public int Rank { get; set; }
        [AllowNull]
        public string Caption { get; set; }
        public string Content { get; set; }

        public int AdvertisementId { get; set; }
        public virtual Advertisement Advertisement { get; set; }
    }
}
