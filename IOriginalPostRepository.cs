using Models.OriginalPost;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.OriginalPostRepository
{
    public interface IOriginalPostRepository
    {
        Task<OriginalPost> GetByIdAsync(Guid id);
        Task<IEnumerable<OriginalPost>> GetAllAsync();
        Task CreateAsync(OriginalPost post);
        Task UpdateAsync(Guid id, OriginalPost post);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<OriginalPost>> GetPostsByUsernameAsync(string username);
        Task<IEnumerable<OriginalPost>> GetPostsFromUsersAsync(IEnumerable<string> usernames);
        Task DeletePostsByUsernameAsync(string username);

        Task<IEnumerable<OriginalPost>> GetPostsByHashtagsAsync(List<string> hashtags);
    }
}
