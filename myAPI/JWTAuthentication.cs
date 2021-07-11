using System;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Web.Services3.Security.Utility;
using myAPI.Controllers;

namespace myAPI
{
    public class JWTAuthentication : IJWTAuthentication
    {
        //private IDictionary<string, string> users = new Dictionary<string, string>
        //{ {"test", "test"},
        //  {"test2", "test2"} 
        //};

        private string key;

        //constructor that passes in the key
        public JWTAuthentication(string key)
        {
            this.key = key;
        }

        public string Authenticate(string username, string password)
        {
            //if(!users.Any(u => u.Key == username && u.Value == password))
            //{
            //    return null;
            //}

            if(username != "test" || password != "test123")
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
