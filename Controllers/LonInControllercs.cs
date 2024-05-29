using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogInController : ControllerBase
    {
        [HttpPost]
        public IActionResult LogIn([FromBody] UserCredentials credentials)
        {
            if (credentials.Username == "user" && credentials.Password == "password")
            {
                var token = TokenStore.GenerateToken();

                return Ok(new { token });
            }

            return Unauthorized("Invalid username or password");
        }
    }
}
