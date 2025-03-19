using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Models.Post
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)] // Store Guid as a string in MongoDB
        public Guid PostId { get; set; }

        [BsonElement("caption")]
        public string Caption { get; set; }

        [BsonElement("imageUrl")]
        public string ImageUrl { get; set; }

        [BsonElement("likes")]
        public List<string> Likes { get; set; } = new List<string>(); // Store usernames as strings

        [BsonElement("comments")]
        public List<Guid> Comments { get; set; } = new List<Guid>(); // Store comment IDs as a list of Guids

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("createdBy")]
        public string CreatedBy { get; set; } // Store username as string
    }
}
