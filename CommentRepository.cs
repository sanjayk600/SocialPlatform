using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Data;
using Models.Comments;

namespace Repositories.CommentRepository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IMongoCollection<Comments> _collection;

        public CommentRepository(MongoDBContext context)
        {
            _collection = context.GetCollection<Comments>("Comments");
        }

        public async Task<Comments> GetByIdAsync(Guid id)
        {
           
            return await _collection.Find(Builders<Comments>.Filter.Eq("_id", id.ToString())).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Comments>> GetAllAsync()
        {
            return await _collection.Find(Builders<Comments>.Filter.Empty).ToListAsync();
        }

        public async Task CreateAsync(Comments comment)
        {
            await _collection.InsertOneAsync(comment);
        }

      
        public async Task UpdateAsync(Guid id, Comments comment)
        {
            // Convert Guid to string for MongoDB filtering
            await _collection.ReplaceOneAsync(Builders<Comments>.Filter.Eq("_id", id.ToString()), comment);
        }

        // Update the method to use Guid for id
        public async Task DeleteAsync(Guid id)
        {
            // Convert Guid to string for MongoDB filtering
            await _collection.DeleteOneAsync(Builders<Comments>.Filter.Eq("_id", id.ToString()));
        }

        // Update the method to use Guid for postId
        public async Task<IEnumerable<Comments>> GetCommentsByPostIdAsync(Guid postId)
        {
            // Convert Guid to string for MongoDB filtering
            return await _collection.Find(Builders<Comments>.Filter.Eq("postId", postId.ToString())).ToListAsync();
        }
    }
}
