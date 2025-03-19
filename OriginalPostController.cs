using Microsoft.AspNetCore.Mvc;
using Models.OriginalPost;
using Services.OriginalPostServices;
using System;
using System.Threading.Tasks;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OriginalPostController : ControllerBase
    {
        private readonly IOriginalPostService _postService;

        public OriginalPostController(IOriginalPostService postService)
        {
            _postService = postService;
        }

        // GET: api/originalpost/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        // GET: api/originalpost
        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }

        // POST: api/originalpost
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] OriginalPost post)
        {
            if (post == null)
            {
                return BadRequest("Post is null");
            }

            await _postService.CreatePostAsync(post);
            return CreatedAtAction(nameof(GetPostById), new { id = post.PostId }, post);
        }

        // PUT: api/originalpost/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(Guid id, [FromBody] OriginalPost post)
        {
            if (post == null || id != post.PostId)
            {
                return BadRequest("Post data is invalid");
            }

            var existingPost = await _postService.GetPostByIdAsync(id);
            if (existingPost == null)
            {
                return NotFound();
            }

            await _postService.UpdatePostAsync(id, post);
            return NoContent();
        }

        // DELETE: api/originalpost/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var existingPost = await _postService.GetPostByIdAsync(id);
            if (existingPost == null)
            {
                return NotFound();
            }

            await _postService.DeletePostAsync(id);
            return NoContent();
        }

        // GET: api/originalpost/user/{username}
        [HttpGet("user/{username}")]
        public async Task<IActionResult> GetPostsByUsername(string username)
        {
            var posts = await _postService.GetPostsByUsernameAsync(username);
            if (posts == null)
            {
                return NotFound();
            }

            return Ok(posts);
        }

        // GET: api/originalpost/following/{userId}
        [HttpGet("following/{userId}")]
        public async Task<IActionResult> GetPostsFromFollowing(Guid userId)
        {
            var posts = await _postService.GetPostsFromFollowingAsync(userId);
            if (posts == null)
            {
                return NotFound("No posts found from followed users.");
            }

            return Ok(posts);
        }

        // PATCH: api/originalpost/{id}/like
        [HttpPatch("{id}/like")]
        public async Task<IActionResult> LikePost(Guid id, [FromQuery] string username)
        {
            var result = await _postService.LikePostAsync(id, username);
            if (!result)
            {
                return NotFound("Post not found or already liked by user.");
            }

            return NoContent();
        }

        // PATCH: api/originalpost/{id}/unlike
        [HttpPatch("{id}/unlike")]
        public async Task<IActionResult> UnlikePost(Guid id, [FromQuery] string username)
        {
            var result = await _postService.UnlikePostAsync(id, username);
            if (!result)
            {
                return NotFound("Post not found or user hasn't liked the post.");
            }

            return NoContent();
        }

        // DELETE: api/originalpost/user/{username}
        [HttpDelete("user/{username}")]
        public async Task<IActionResult> DeletePostsByUsername(string username)
        {
            await _postService.DeletePostsByUsernameAsync(username);
            return NoContent();
        }


        // GET: api/originalpost/hashtags
        [HttpGet("hashtags")]
        public async Task<IActionResult> GetPostsByHashtags([FromQuery] List<string> hashtags)
        {
            if (hashtags == null || !hashtags.Any())
            {
                return BadRequest("Hashtags list is null or empty");
            }

            var posts = await _postService.GetPostsByHashtagsAsync(hashtags);
            if (posts == null || !posts.Any())
            {
                return NotFound("No posts found with the given hashtags.");
            }

            return Ok(posts);
        }

    }
}
