using CollegeApi.Models;
using CollegeApi.Models.Request;
using CollegeApi.MongoSettings;
using CollegeApi.Services.Interface;
using MongoDB.Driver;

namespace CollegeApi.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> users;

        public UserService(DbContext context)
        {
            users = context.Users;
        }

        public async Task<List<User>> GetUsers()
        {
            return await users.Find(_ => true).ToListAsync();
        }
        public async Task<User> GetUserById(string id)
        {
            return await users.Find(u => u.Id == id).FirstOrDefaultAsync();
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await users.Find(u => u.Email == email).FirstOrDefaultAsync();
        }
        public async Task<User> CreateUser(User user)
        {
            await users.InsertOneAsync(user);
            return user;
        }
        public async Task<User> UpdateUser(string id, User user)
        {
            await users.ReplaceOneAsync(u => u.Id == id, user);
            return user;
        }
        public async Task<User> DeleteUser(string id)
        {
            await users.DeleteOneAsync(u => u.Id == id);
            var user = await users.Find(u => u.Id == id).FirstOrDefaultAsync();
            return user;
        }
        public async Task<User> AuthenticateUser(UserRequest userRequest)
        {
            return await users.Find(u => u.Email == userRequest.Email && u.Password == userRequest.Password).FirstOrDefaultAsync();
        }
    }
}
