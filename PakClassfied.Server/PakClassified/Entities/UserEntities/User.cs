using PakClassified.Entities.PakClassified;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PakClassified.Entities.UserEntities
{
    public class User : BaseEntity<int>, INamedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [AllowNull]
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; }
        [AllowNull]
        public string ContactNumber { get; set; }
        public string Email { get; set; }

        [AllowNull]
        public string Image { get; set; }
        [AllowNull]
        public string SecurityQuestion { get; set; }
        [AllowNull]
        public string SecurityAnswer { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Advertisement> Advertisements { get; set; }
    }
}
