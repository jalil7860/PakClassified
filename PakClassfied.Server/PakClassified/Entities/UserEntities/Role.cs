using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakClassified.Entities.UserEntities
{
    public class Role : BaseEntity<int>, INamedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [AllowNull]
        public int Rank { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
