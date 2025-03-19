using Models.Post;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.PostServices
{
    public interface IPostServices
    {
        Task<Post> GetPostByIdAsync(Guid id);
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task CreatePostAsync(Post post);
        Task UpdatePostAsync(Guid id, Post post);
        Task DeletePostAsync(Guid id);
        Task<IEnumerable<Post>> GetPostsByUsernameAsync(string username);
        Task<IEnumerable<Post>> GetPostsFromFollowingAsync(Guid userId);
        Task<bool> LikePostAsync(Guid postId, string username); // New method
        Task<bool> UnlikePostAsync(Guid postId, string username); // New method

        Task DeletePostsByUsernameAsync(string username);
    }
}
