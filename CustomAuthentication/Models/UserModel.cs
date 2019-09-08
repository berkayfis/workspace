using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CustomAuthentication.Models
{
    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonElement("Username")]
        public string UserName { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }
    }
}
