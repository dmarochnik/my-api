using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace myAPI.Models
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public string Username { get; set; }

        public string Token { get; set; }

        public long Exp { get; set; }
    }
}
