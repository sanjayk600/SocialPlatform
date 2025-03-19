using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Models.Comments
{
    public class Comments
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        [BsonElement("comment")]
        public string CommentText { get; set; }

        [BsonElement("createdBy")]
        public string CreatedBy { get; set; }  // User ID

        [BsonElement("postId")]
        [BsonRepresentation(BsonType.String)]
        public Guid PostId { get; set; }  // Post ID

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}
