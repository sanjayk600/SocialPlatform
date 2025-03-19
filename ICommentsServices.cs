using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Comments;

namespace Services.CommentServices
{
    public interface ICommentServices
    {
        Task<Comments> GetCommentByIdAsync(Guid id);
        Task<IEnumerable<Comments>> GetAllCommentsAsync();
        Task CreateCommentAsync(Comments comment);
        Task UpdateCommentAsync(Guid id, Comments comment);
        Task DeleteCommentAsync(Guid id);
        Task<IEnumerable<Comments>> GetCommentsByPostIdAsync(Guid postId);
    }
}
