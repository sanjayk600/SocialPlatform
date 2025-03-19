using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.User;

namespace Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid id);
        Task<IEnumerable<User>> GetAllAsync();
        Task CreateAsync(User user);
        Task UpdateAsync(Guid id, User user);
        Task DeleteAsync(Guid id);
        Task<User> GetByUsernameAsync(string username);

        // New methods for follow/unfollow
        Task FollowUserAsync(Guid userId, Guid followUserId);
        Task UnfollowUserAsync(Guid userId, Guid unfollowUserId);

        // Add this method for partial update
        Task UpdatePartialAsync(Guid id, UpdateUserDto userDto);
    }
}


