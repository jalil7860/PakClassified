using PakClassified.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models.UserModels.Auth
{
    public class SignupModel
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
        public RoleModel Role { get; set; }
    }
}
