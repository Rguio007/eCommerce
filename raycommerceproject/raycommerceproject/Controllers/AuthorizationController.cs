using Microsoft.AspNetCore.Mvc;
using raycommerceproject.Models;
using raycommerceproject.Services;

namespace raycommerceproject.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IUserService _userService; 

        public AuthorizationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] Register model)
        {
            var user = new User
            {
                Email = model.Email,
                PasswordHash = model.Password
            };

            _userService.RegisterUser(user);

            var token = _userService.GenerateJwtToken(user);

            return Ok(new { Message = "Registration successful!", Token = token });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LogIn model)
        {
            var user = _userService.AuthenticateUser(model.Email, model.Password);

            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid credentials" });
            }

            var token = _userService.GenerateJwtToken(user);

            return Ok(new { Message = "Log In successfully!", Token = token });
        }
    }

}