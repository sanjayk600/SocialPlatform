using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Comments;
using Repositories;
using Repositories.CommentRepository;

namespace Services.CommentServices
{
    public class CommentServices : ICommentServices
    {
        private readonly ICommentRepository _commentRepository;

       
        public CommentServices(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<Comments> GetCommentByIdAsync(Guid id)
        {
            return await _commentRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Comments>> GetAllCommentsAsync()
        {
            return await _commentRepository.GetAllAsync();
        }

        public async Task CreateCommentAsync(Comments comment)
        {
            await _commentRepository.CreateAsync(comment);
        }

        public async Task UpdateCommentAsync(Guid id, Comments comment)
        {
            await _commentRepository.UpdateAsync(id, comment);
        }

        public async Task DeleteCommentAsync(Guid id)
        {
            await _commentRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Comments>> GetCommentsByPostIdAsync(Guid postId)
        {
            return await _commentRepository.GetCommentsByPostIdAsync(postId);
        }
    }
}
