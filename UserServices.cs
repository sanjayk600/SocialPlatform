using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.User;
using Repositories;
using Repositories.UserRepository;

namespace Services.UserServices
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task CreateUserAsync(User user)
        {
            await _userRepository.CreateAsync(user);
        }

        public async Task UpdateUserAsync(Guid id, User user)
        {
            await _userRepository.UpdateAsync(id, user);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetByUsernameAsync(username);
        }

        public async Task FollowUserAsync(Guid userId, Guid followUserId)
        {
            await _userRepository.FollowUserAsync(userId, followUserId);
        }

        public async Task UnfollowUserAsync(Guid userId, Guid unfollowUserId)
        {
            await _userRepository.UnfollowUserAsync(userId, unfollowUserId);
        }

        public async Task<bool> FollowUserByUsernameAsync(string username, string followUsername)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            var followUser = await _userRepository.GetByUsernameAsync(followUsername);

            if (user == null || followUser == null)
                return false;

            await _userRepository.FollowUserAsync(user.UserId, followUser.UserId);
            return true;
        }

        public async Task<bool> UnfollowUserByUsernameAsync(string username, string unfollowUsername)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            var unfollowUser = await _userRepository.GetByUsernameAsync(unfollowUsername);

            if (user == null || unfollowUser == null)
                return false;

            await _userRepository.UnfollowUserAsync(user.UserId, unfollowUser.UserId);
            return true;
        }


        public async Task UpdatePartialUserAsync(Guid id, UpdateUserDto userDto)
        {
            await _userRepository.UpdatePartialAsync(id, userDto);
        }



    }
}
