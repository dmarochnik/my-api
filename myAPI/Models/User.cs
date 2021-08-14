using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace myAPI.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        public string Fname { get; set; }

        public string Lname { get; set; }

        public string Phone { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }

    public class UserResponse
    {
        public string Fname { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Error { get; set; }
    }
}
