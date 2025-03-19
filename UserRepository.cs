using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Models.User;
using Data;

namespace Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _collection;

        public UserRepository(MongoDBContext context)
        {
            _collection = context.GetCollection<User>("Users");
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _collection.Find(Builders<User>.Filter.Eq(u => u.UserId, id)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _collection.Find(Builders<User>.Filter.Empty).ToListAsync();
        }

        public async Task CreateAsync(User user)
        {
            // Ensure followers and following fields are initialized as arrays
            user.Followers ??= new List<Guid>();
            user.Following ??= new List<Guid>();
            user.Posts ??= new List<Guid>();

            await _collection.InsertOneAsync(user);
        }

        public async Task UpdateAsync(Guid id, User user)
        {
            await _collection.ReplaceOneAsync(Builders<User>.Filter.Eq(u => u.UserId, id), user);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _collection.DeleteOneAsync(Builders<User>.Filter.Eq(u => u.UserId, id));
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _collection.Find(Builders<User>.Filter.Eq(u => u.Username, username)).FirstOrDefaultAsync();
        }

        // Follow user logic
        public async Task FollowUserAsync(Guid userId, Guid followUserId)
        {
            // Initialize 'following' and 'followers' fields if they are null
            var initializeUserFields = Builders<User>.Update.Combine(
                Builders<User>.Update.SetOnInsert(u => u.Following, new List<Guid>()),
                Builders<User>.Update.SetOnInsert(u => u.Followers, new List<Guid>())
            );

            await _collection.UpdateOneAsync(
                Builders<User>.Filter.Eq(u => u.UserId, userId),
                initializeUserFields
            );

            await _collection.UpdateOneAsync(
                Builders<User>.Filter.Eq(u => u.UserId, followUserId),
                initializeUserFields
            );

            // Now safely add to set
            var filterUser = Builders<User>.Filter.Eq(u => u.UserId, userId);
            var updateUser = Builders<User>.Update.AddToSet(u => u.Following, followUserId);

            var filterFollowUser = Builders<User>.Filter.Eq(u => u.UserId, followUserId);
            var updateFollowUser = Builders<User>.Update.AddToSet(u => u.Followers, userId);

            // Perform both updates
            await _collection.UpdateOneAsync(filterUser, updateUser);
            await _collection.UpdateOneAsync(filterFollowUser, updateFollowUser);
        }

        // Unfollow user logic
        public async Task UnfollowUserAsync(Guid userId, Guid unfollowUserId)
        {
            // Ensure 'following' field is an array for the user
            var initializeUserFields = Builders<User>.Update.Combine(
                Builders<User>.Update.SetOnInsert(u => u.Following, new List<Guid>())
            );

            await _collection.UpdateOneAsync(
                Builders<User>.Filter.Eq(u => u.UserId, userId),
                initializeUserFields
            );

            // Ensure 'followers' field is an array for the user to unfollow
            var initializeUnfollowUserFields = Builders<User>.Update.Combine(
                Builders<User>.Update.SetOnInsert(u => u.Followers, new List<Guid>())
            );

            await _collection.UpdateOneAsync(
                Builders<User>.Filter.Eq(u => u.UserId, unfollowUserId),
                initializeUnfollowUserFields
            );

            // Apply the pull operation after ensuring arrays are present
            var filterUser = Builders<User>.Filter.Eq(u => u.UserId, userId);
            var updateUser = Builders<User>.Update.Pull(u => u.Following, unfollowUserId);

            var filterUnfollowUser = Builders<User>.Filter.Eq(u => u.UserId, unfollowUserId);
            var updateUnfollowUser = Builders<User>.Update.Pull(u => u.Followers, userId);

            // Perform both updates
            await _collection.UpdateOneAsync(filterUser, updateUser);
            await _collection.UpdateOneAsync(filterUnfollowUser, updateUnfollowUser);
        }


        public async Task UpdatePartialAsync(Guid id, UpdateUserDto userDto)
        {
            var updateDefinitions = new List<UpdateDefinition<User>>();

      
            if (!string.IsNullOrEmpty(userDto.Username))
            {
                Console.Write("inside username change");
                updateDefinitions.Add(Builders<User>.Update.Set(u => u.Username, userDto.Username));
            }
            if (!string.IsNullOrEmpty(userDto.Email))
            {
                Console.Write("inside email change");
                updateDefinitions.Add(Builders<User>.Update.Set(u => u.Email, userDto.Email));
            }
            if (!string.IsNullOrEmpty(userDto.FullName))
            {
                Console.Write("inside fullname change");
                updateDefinitions.Add(Builders<User>.Update.Set(u => u.FullName, userDto.FullName));
            }
            if (!string.IsNullOrEmpty(userDto.ProfilePictureUrl))
            {
                Console.Write("inside username change");
                updateDefinitions.Add(Builders<User>.Update.Set(u => u.ProfilePictureUrl, userDto.ProfilePictureUrl));
            }
            if (!string.IsNullOrEmpty(userDto.Bio))
            {
                Console.Write("inside username change");
                updateDefinitions.Add(Builders<User>.Update.Set(u => u.Bio, userDto.Bio));
            }
            var combinedUpdate = Builders<User>.Update.Combine(updateDefinitions);

       
            await _collection.UpdateOneAsync(
                Builders<User>.Filter.Eq(u => u.UserId, id),
                combinedUpdate
            );
        }



    }
}
