using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.User;
using Services.TokenService;
using Services.UserServices;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly TokenService _tokenService;
        private string username;

        public AuthController(IUserServices userServices, TokenService tokenService)
        {
            _userServices = userServices;
            _tokenService = tokenService;
        }

        // Register a new user
        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string password, string email, string fullName)
        {
            var existingUser = await _userServices.GetUserByUsernameAsync(username);
            if (existingUser != null)
            {
                return BadRequest("Username is already taken.");
            }

            var user = new User
            {
                UserId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Username = username,
                Email = email,
                FullName = fullName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password) // Hash the password
            };

            await _userServices.CreateUserAsync(user);
            return Ok("User registered successfully.");
        }

        // Login to get JWT token
        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userServices.GetUserByUsernameAsync(username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)) // Verify the password
            {
                return Unauthorized("Invalid credentials.");
            }

            var token = _tokenService.GenerateToken(user);
            return Ok(new { token });
        }

       

    }
}
