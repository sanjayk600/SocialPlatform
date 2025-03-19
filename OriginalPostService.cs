using Models.OriginalPost;
using Repositories;
using Repositories.OriginalPostRepository;
using Repositories.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.OriginalPostServices
{
    public class OriginalPostService : IOriginalPostService
    {
        private readonly IOriginalPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public OriginalPostService(IOriginalPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task<OriginalPost> GetPostByIdAsync(Guid id)
        {
            return await _postRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<OriginalPost>> GetAllPostsAsync()
        {
            return await _postRepository.GetAllAsync();
        }

        public async Task CreatePostAsync(OriginalPost post)
        {
            await _postRepository.CreateAsync(post);
        }

        public async Task UpdatePostAsync(Guid id, OriginalPost post)
        {
            await _postRepository.UpdateAsync(id, post);
        }

        public async Task DeletePostAsync(Guid id)
        {
            await _postRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<OriginalPost>> GetPostsByUsernameAsync(string username)
        {
            return await _postRepository.GetPostsByUsernameAsync(username);
        }

        public async Task<IEnumerable<OriginalPost>> GetPostsFromFollowingAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null || user.Following == null || !user.Following.Any())
            {
                return Enumerable.Empty<OriginalPost>();
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


        public async Task<IEnumerable<OriginalPost>> GetPostsByHashtagsAsync(List<string> hashtags)
        {
            return await _postRepository.GetPostsByHashtagsAsync(hashtags);
        }

    }
}
