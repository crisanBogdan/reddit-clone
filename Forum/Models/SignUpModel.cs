using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class SignUpModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string RepeatedPassword { get; set; }
        public string ErrorMessage { get; set; }
    }
}
