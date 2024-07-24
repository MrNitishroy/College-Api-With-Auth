using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CollegeApi.Models.Request
{
    public class UserRequest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
       
        public string Email { get; set; }
        public string Password { get; set; }
       
    }
}
