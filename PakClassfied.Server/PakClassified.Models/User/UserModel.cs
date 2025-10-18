using PakClassified.Models.PakClassified;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakClassified.Models.User
{
    public class UserModel : BaseDTO<int>
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; }
        public string? ContactNumber { get; set; }
        public string Email { get; set; }
        public string? Image { get; set; }
        public string? SecurityQuestion { get; set; }
        public string? SecurityAnswer { get; set; }
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public ICollection<AdvertisementModel>? Advertisements { get; set; }
    }
}
