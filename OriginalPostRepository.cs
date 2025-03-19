using Data;
using Models.OriginalPost;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.OriginalPostRepository
{
    public class OriginalPostRepository : IOriginalPostRepository
    {
        private readonly IMongoCollection<OriginalPost> _collection;

        public OriginalPostRepository(MongoDBContext context)
        {
            _collection = context.GetCollection<OriginalPost>("OriginalPosts");
        }

        public async Task<OriginalPost> GetByIdAsync(Guid id)
        {
            return await _collection.Find(Builders<OriginalPost>.Filter.Eq("_id", id.ToString())).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<OriginalPost>> GetAllAsync()
        {
            return await _collection.Find(Builders<OriginalPost>.Filter.Empty).ToListAsync();
        }

        public async Task CreateAsync(OriginalPost post)
        {
            await _collection.InsertOneAsync(post);
        }

        public async Task UpdateAsync(Guid id, OriginalPost post)
        {
            await _collection.ReplaceOneAsync(Builders<OriginalPost>.Filter.Eq("_id", id.ToString()), post);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _collection.DeleteOneAsync(Builders<OriginalPost>.Filter.Eq("_id", id.ToString()));
        }

        public async Task<IEnumerable<OriginalPost>> GetPostsByUsernameAsync(string username)
        {
            return await _collection.Find(Builders<OriginalPost>.Filter.Eq("createdBy", username)).ToListAsync();
        }

        public async Task<IEnumerable<OriginalPost>> GetPostsFromUsersAsync(IEnumerable<string> usernames)
        {
            var filter = Builders<OriginalPost>.Filter.In("createdBy", usernames);
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task DeletePostsByUsernameAsync(string username)
        {
            var filter = Builders<OriginalPost>.Filter.Eq("createdBy", username);
            await _collection.DeleteManyAsync(filter);
        }

        public async Task<IEnumerable<OriginalPost>> GetPostsByHashtagsAsync(List<string> hashtags)
        {
            var filter = Builders<OriginalPost>.Filter.AnyIn("hashtags", hashtags);
            return await _collection.Find(filter).ToListAsync();
        }



    }
}
