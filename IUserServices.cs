using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.User;

namespace Services.UserServices
{
    public interface IUserServices
    {
        Task<User> GetUserByIdAsync(Guid id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(Guid id, User user);
        Task DeleteUserAsync(Guid id);
        Task<User> GetUserByUsernameAsync(string username);

        // Follow/Unfollow methods
        Task FollowUserAsync(Guid userId, Guid followUserId);
        Task UnfollowUserAsync(Guid userId, Guid unfollowUserId);



        // Follow/Unfollow by Username methods
        Task<bool> FollowUserByUsernameAsync(string username, string followUsername);
        Task<bool> UnfollowUserByUsernameAsync(string username, string unfollowUsername);


        // Partial update method
        Task UpdatePartialUserAsync(Guid id, UpdateUserDto userDto);
    }
}

