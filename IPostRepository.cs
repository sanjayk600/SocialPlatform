using Models.Post;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IPostRepository
    {
        Task<Post> GetByIdAsync(Guid id);
        Task<IEnumerable<Post>> GetAllAsync();
        Task CreateAsync(Post post);
        Task UpdateAsync(Guid id, Post post);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Post>> GetPostsByUsernameAsync(string username);
        Task<IEnumerable<Post>> GetPostsFromUsersAsync(IEnumerable<string> usernames);
        Task DeletePostsByUsernameAsync(string username);
    }
}
