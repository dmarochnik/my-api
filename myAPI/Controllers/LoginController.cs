using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace myAPI.Controllers
{
    //Anything in a square bracket like this is an annotation. It gives the LoginController class additional functionality so that it can act as an API and process web requests.
    [ApiController]
    //drops the word controller from the url path so the endpoint is just login 
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        //interface ILogger allows different controllers to log different events
        private readonly ILogger<LoginController> _logger;

        //interface IJWTAuthentication allows different controllers to authenticate different data. 
        private readonly IJWTAuthentication jWTAuthentication;

        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        //logger is like Console.WriteLine, it is an message that gets printed to the console
        public LoginController(ILogger<LoginController> logger, IJWTAuthentication jWTAuthentication, DbContextOptions<ApplicationDbContext> options)
        {
            this._logger = logger;
            this.jWTAuthentication = jWTAuthentication;
            this._dbContextOptions = options;
        }

/*        [HttpPost]
        public IActionResult Login(LoginRequest request)
        {
            if(request.Username == "test" && request.Password == "test")
            {
                return Ok(request);
            }
            if (request.Username == null || request.Password == null)
            {
                return BadRequest();
            }
            if (request.Username != "test" || request.Password != "test")
            {
                return Unauthorized();
            }

            return NotFound();
        }*/

        [HttpPost]
        //method that returns status codes in Microsoft builtin interface IActionResult, and passes in request as the data being entered in the request body in Postman
        public IActionResult Login(LoginRequest request)
        {
            try
            {
                // This should be database logic eventually
                if (request.Username == "test" && BCrypt.Net.BCrypt.Verify(request.Password, "$2a$11$yfTzWBzWR9.KRVpa.SPVe.q3QXkrUfc7ZGHBuKuX3XEbHczix1H8e"))
                {
                    //returns OK with loginresponse wrapped inside of it
                    return Ok(new LoginResponse
                    {
                        Username = request.Username,
                        Exp = DateTimeOffset.UtcNow.AddHours(.5).ToUnixTimeMilliseconds(),
                        Token = jWTAuthentication.Authenticate(request.Username, request.Password)
                    });
                }
                if (string.IsNullOrEmpty(request.Username))
                {
                    // Fill out response with error message as per API  contract
                    //err is what is getting returned to the user, LogError is getting stored in the system?
                    return Unauthorized(ErrorHandler.OnError("Please enter username", StatusCodes.Status401Unauthorized, _logger));

                }
                if (string.IsNullOrEmpty(request.Password))
                {
                    // Fill out response with error message as per API  contract
                    //err is what is getting returned to the user, LogError is getting stored in the system?
                    return Unauthorized(ErrorHandler.OnError("Please enter password", StatusCodes.Status401Unauthorized, _logger));

                }
                if (request.Username != "test" || request.Password != "test123")
                {
                    // Fill out response with error message as per API contract
                    return Unauthorized(ErrorHandler.OnError("Incorrect username or password", StatusCodes.Status401Unauthorized, _logger));
                }
            }
            catch(TimeoutException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorHandler.OnError("Timeout occurred on Login", StatusCodes.Status500InternalServerError, _logger, ex));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorHandler.OnError("Unknown error occurred", StatusCodes.Status500InternalServerError, _logger, ex));
            }

            return NotFound();
        }
    }
}
