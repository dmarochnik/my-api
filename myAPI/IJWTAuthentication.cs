using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myAPI.Controllers;
using myAPI.Models;

namespace myAPI
{
    public interface IJWTAuthentication
    {
        public string Authenticate(string username, string password);
    }
}
