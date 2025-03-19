using Models.Post;
using Repositories;
using Repositories.PostRepository;
using Repositories.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.PostServices
{
    public class PostServices : IPostServices
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public PostServices(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task<Post> GetPostByIdAsync(Guid id)
        {
            return await _postRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _postRepository.GetAllAsync();
        }

        public async Task CreatePostAsync(Post post)
        {
            await _postRepository.CreateAsync(post);
        }

        public async Task UpdatePostAsync(Guid id, Post post)
        {
            await _postRepository.UpdateAsync(id, post);
        }

        public async Task DeletePostAsync(Guid id)
        {
            await _postRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Post>> GetPostsByUsernameAsync(string username)
        {
            return await _postRepository.GetPostsByUsernameAsync(username);
        }

        public async Task<IEnumerable<Post>> GetPostsFromFollowingAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null || user.Following == null || !user.Following.Any())
            {
                return Enumerable.Empty<Post>();
            }

            var followingUsernames = user.Following.Select(followedUserId => _userRepository.GetByIdAsync(followedUserId).Result.Username).ToList();
            return await _postRepository.GetPostsFromUsersAsync(followingUsernames);
        }

        public async Task<bool> LikePostAsync(Guid postId, string username)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null || post.Likes.Contains(username))
            {
                return false; 
            }

            post.Likes.Add(username); 
            await _postRepository.UpdateAsync(postId, post); 
            return true;
        }

        public async Task<bool> UnlikePostAsync(Guid postId, string username)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null || !post.Likes.Contains(username))
            {
                return false; 
            }

            post.Likes.Remove(username); 
            await _postRepository.UpdateAsync(postId, post); 
            return true;
        }


        public async Task DeletePostsByUsernameAsync(string username)
        {
            await _postRepository.DeletePostsByUsernameAsync(username);
        }
    }
}
