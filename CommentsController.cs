using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Comments;
using Services.CommentServices;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentServices _commentServices;

        // Constructor injection for the comment services
        public CommentsController(ICommentServices commentServices)
        {
            _commentServices = commentServices;
        }

        // GET: api/comments/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Comments>> GetCommentById(Guid id)
        {
            var comment = await _commentServices.GetCommentByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        // GET: api/comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comments>>> GetAllComments()
        {
            var comments = await _commentServices.GetAllCommentsAsync();
            return Ok(comments);
        }

        // POST: api/comments
        [HttpPost]
        public async Task<ActionResult> CreateComment([FromBody] Comments comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _commentServices.CreateCommentAsync(comment);
            return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment);
        }

        // PUT: api/comments/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateComment(Guid id, [FromBody] Comments comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingComment = await _commentServices.GetCommentByIdAsync(id);
            if (existingComment == null)
            {
                return NotFound();
            }

            await _commentServices.UpdateCommentAsync(id, comment);
            return NoContent();
        }

        // DELETE: api/comments/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComment(Guid id)
        {
            var comment = await _commentServices.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            await _commentServices.DeleteCommentAsync(id);
            return NoContent();
        }

        // GET: api/comments/post/{postId}
        [HttpGet("post/{postId}")]
        public async Task<ActionResult<IEnumerable<Comments>>> GetCommentsByPostId(Guid postId)
        {
            var comments = await _commentServices.GetCommentsByPostIdAsync(postId);
            if (comments == null)
            {
                return NotFound();
            }

            return Ok(comments);
        }
    }
}
