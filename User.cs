using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Models.User
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid UserId { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("fullName")]
        public string FullName { get; set; }

        [BsonElement("profilePictureUrl")]
        public string ProfilePictureUrl { get; set; }

        [BsonElement("bio")]
        public string Bio { get; set; }

        // Ensure these fields are initialized as empty lists
        [BsonElement("followers")]
        public List<Guid> Followers { get; set; } = new List<Guid>();

        [BsonElement("following")]
        public List<Guid> Following { get; set; } = new List<Guid>();

        [BsonElement("posts")]
        public List<Guid> Posts { get; set; } = new List<Guid>();

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("passwordHash")]
        public string PasswordHash { get; set; }
    }
}
