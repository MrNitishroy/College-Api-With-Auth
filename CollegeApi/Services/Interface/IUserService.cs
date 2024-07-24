using CollegeApi.Models;
using CollegeApi.Models.Request;

namespace CollegeApi.Services.Interface
{
    public interface IUserService
    {
        public Task<List<User>> GetUsers();
        public Task<User> GetUserById(string id);
        public Task<User> GetUserByEmail(string email);
        public Task<User> CreateUser(User user);
        public Task<User> UpdateUser(string id, User user);
        public Task<User> DeleteUser(string id);
        public Task<User> AuthenticateUser(UserRequest userRequest);
    }
}
