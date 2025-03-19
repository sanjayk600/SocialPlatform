using Data;
using Models.Post;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.PostRepository
{
    public class PostRepository : IPostRepository
    {
        private readonly IMongoCollection<Post> _collection;

        public PostRepository(MongoDBContext context)
        {
            _collection = context.GetCollection<Post>("Posts");
        }

        public async Task<Post> GetByIdAsync(Guid id)
        {
            return await _collection.Find(Builders<Post>.Filter.Eq("_id", id.ToString())).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _collection.Find(Builders<Post>.Filter.Empty).ToListAsync();
        }

        public async Task CreateAsync(Post post)
        {
            await _collection.InsertOneAsync(post);
        }

        public async Task UpdateAsync(Guid id, Post post)
        {
            await _collection.ReplaceOneAsync(Builders<Post>.Filter.Eq("_id", id.ToString()), post);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _collection.DeleteOneAsync(Builders<Post>.Filter.Eq("_id", id.ToString()));
        }

        public async Task<IEnumerable<Post>> GetPostsByUsernameAsync(string username)
        {
            return await _collection.Find(Builders<Post>.Filter.Eq("createdBy", username)).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsFromUsersAsync(IEnumerable<string> usernames)
        {
            var filter = Builders<Post>.Filter.In("createdBy", usernames);
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task DeletePostsByUsernameAsync(string username)
        {
            var filter = Builders<Post>.Filter.Eq("createdBy", username);
            await _collection.DeleteManyAsync(filter);
        }
    }
}
