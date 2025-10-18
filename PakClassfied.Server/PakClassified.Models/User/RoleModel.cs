using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakClassified.Models.User
{
    public class RoleModel : BaseDTO<int>
    {
        public string Name { get; set; }
        public int Rank { get; set; }
    }
}
