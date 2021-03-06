using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myAPI.Models;
using Microsoft.AspNetCore.Http;

namespace myAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly ILogger<RegisterController> _logger;

        public RegisterController(ILogger<RegisterController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Register(Register reg)
        {
            try
            {
                if (reg.Fname != null && reg.Lname != null && reg.Phone != null && reg.Username != null && reg.Password != null)
                {
                    if (reg.Password.Length < 6)
                    {
                        return BadRequest(new RegisterResponse
                        {
                            Error = "Password must be at least 6 characters",
                        });
                    }
                    if (reg.Password.Length >= 6 )
                    {
                        reg.Password = BCrypt.Net.BCrypt.HashPassword(reg.Password);
                    }
                    return Ok(new RegisterResponse
                    {
                        Fname = reg.Fname,
                        Username = reg.Username,
                        Password = reg.Password
                    });
                }
                if (reg.Fname == null || reg.Lname == null || reg.Phone == null || reg.Username == null || reg.Password == null)
                {
                    return BadRequest(new RegisterResponse
                    {
                        Error = "All fields are required",
                    });
                }
            }
            catch (TimeoutException ex)
            {
                _logger.LogError("Connection timeout exception in login handler: {0}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on login: {0}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NotFound();
        }
    }
}
