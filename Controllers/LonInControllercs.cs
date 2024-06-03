using LibraryAPI.Classes;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        [HttpPost]
        [Consumes ("application/xml", "application/json", "application/x-www-form-urlencoded")]

        public IActionResult LogIn([FromBody] UserCredentials credentials)
        {
            if (credentials.Username == "user" && credentials.Password == "password")
            {
                var token = TokenStore.GenerateToken();

                Request.Headers.Append("authorisation", token);


                return Ok(new { token });
            }

            return Unauthorized("Invalid username or password");
        }
    }
}
