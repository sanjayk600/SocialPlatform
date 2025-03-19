using Microsoft.AspNetCore.Mvc;
using Models.User;
using Services.UserServices;
using System;
using System.Threading.Tasks;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userServices.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        // GET: api/user
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userServices.GetAllUsersAsync();
            return Ok(users);
        }

        // POST: api/user
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest("User is null");

            await _userServices.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
        }

        // PUT: api/user/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] User user)
        {
            if (user == null || id != user.UserId)
                return BadRequest("Invalid user data");

            var existingUser = await _userServices.GetUserByIdAsync(id);
            if (existingUser == null)
                return NotFound();

            await _userServices.UpdateUserAsync(id, user);
            return NoContent();
        }

        // DELETE: api/user/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var existingUser = await _userServices.GetUserByIdAsync(id);
            if (existingUser == null)
                return NotFound();

            await _userServices.DeleteUserAsync(id);
            return NoContent();
        }

        // GET: api/user/username/{username}
        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await _userServices.GetUserByUsernameAsync(username);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // POST: api/user/{userId}/follow/{followUserId}
        [HttpPost("{userId}/follow/{followUserId}")]
        public async Task<IActionResult> FollowUser(Guid userId, Guid followUserId)
        {
           
            if (userId == followUserId)
                return BadRequest("Users cannot follow themselves.");

            // Follow logic
            await _userServices.FollowUserAsync(userId, followUserId);
            return NoContent();
        }

        // POST: api/user/{userId}/unfollow/{unfollowUserId}
        [HttpPost("{userId}/unfollow/{unfollowUserId}")]
        public async Task<IActionResult> UnfollowUser(Guid userId, Guid unfollowUserId)
        {
         
            if (userId == unfollowUserId)
                return BadRequest("Users cannot unfollow themselves.");

            // Unfollow logic
            await _userServices.UnfollowUserAsync(userId, unfollowUserId);
            return NoContent();
        }


        // POST: api/user/username/{username}/follow/{followUsername}
        [HttpPost("username/{username}/follow/{followUsername}")]
        public async Task<IActionResult> FollowUserByUsername(string username, string followUsername)
        {
            if (username == followUsername)
                return BadRequest("Users cannot follow themselves.");

            var result = await _userServices.FollowUserByUsernameAsync(username, followUsername);
            if (!result)
                return NotFound("Either the user or the follow user does not exist.");

            return NoContent();
        }



        // POST: api/user/username/{username}/unfollow/{unfollowUsername}]
        [HttpPost("username/{username}/unfollow/{unfollowUsername}")]
        public async Task<IActionResult> UnfollowUserByUsername(string username, string unfollowUsername)
        {
            if (username == unfollowUsername)
                return BadRequest("Users cannot unfollow themselves.");

            var result = await _userServices.UnfollowUserByUsernameAsync(username, unfollowUsername);
            if (!result)
                return NotFound("Either the user or the unfollow user does not exist.");

            return NoContent();
        }



        // PATCH: api/user/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePartialUser(Guid id, [FromBody] UpdateUserDto userDto)
        {
            if (userDto == null)
                return BadRequest("Invalid user data.");

            var existingUser = await _userServices.GetUserByIdAsync(id);
            if (existingUser == null)
                return NotFound("User not found.");

            await _userServices.UpdatePartialUserAsync(id, userDto);
            return NoContent();  
        }






    }
}
