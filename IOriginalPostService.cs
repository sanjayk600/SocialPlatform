using Models.OriginalPost;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.OriginalPostServices
{
    public interface IOriginalPostService
    {
        Task<OriginalPost> GetPostByIdAsync(Guid id);
        Task<IEnumerable<OriginalPost>> GetAllPostsAsync();
        Task CreatePostAsync(OriginalPost post);
        Task UpdatePostAsync(Guid id, OriginalPost post);
        Task DeletePostAsync(Guid id);
        Task<IEnumerable<OriginalPost>> GetPostsByUsernameAsync(string username);
        Task<IEnumerable<OriginalPost>> GetPostsFromFollowingAsync(Guid userId);
        Task<bool> LikePostAsync(Guid postId, string username);
        Task<bool> UnlikePostAsync(Guid postId, string username);
        Task DeletePostsByUsernameAsync(string username);

        Task<IEnumerable<OriginalPost>> GetPostsByHashtagsAsync(List<string> hashtags);
    }
}
