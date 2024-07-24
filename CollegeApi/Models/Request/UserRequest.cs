using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CollegeApi.Models.Request
{
    public class UserRequest
    {
      
        public string Email { get; set; }
        public string Password { get; set; }
       
    }
}
