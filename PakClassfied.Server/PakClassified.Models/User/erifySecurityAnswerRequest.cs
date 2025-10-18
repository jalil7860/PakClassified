using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakClassified.Models.User
{
    public class VerifySecurityAnswerRequest
    {
        public string Email { get; set; }
        public string SecurityAnswer { get; set; }
    }
}
