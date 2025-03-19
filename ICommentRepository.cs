using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Comments;

namespace Repositories
{
    public interface ICommentRepository
    {
        Task<Comments> GetByIdAsync(Guid id);  // Use Guid instead of string
        Task<IEnumerable<Comments>> GetAllAsync();
        Task CreateAsync(Comments comment);
        Task UpdateAsync(Guid id, Comments comment);  // Use Guid instead of string
        Task DeleteAsync(Guid id);  // Use Guid instead of string
        Task<IEnumerable<Comments>> GetCommentsByPostIdAsync(Guid postId);  // Use Guid instead of string for postId
    }
}
