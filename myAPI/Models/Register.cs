using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myAPI.Models
{
    public class Register
    {
        public string Fname { get; set; }

        public string Lname { get; set; }

        public string Phone { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }

    public class RegisterResponse
    {
        public string Fname { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Error { get; set; }
    }
}
