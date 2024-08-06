using CollegeApi.Models;
using CollegeApi.Models.Request;
using CollegeApi.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await userService.GetUsers();
            return Ok(users);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            var newUser = await userService.CreateUser(user);
            return Ok(newUser);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] User user)
        {
            var updatedUser = await userService.UpdateUser(id, user);
            return Ok(updatedUser);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var deletedUser = await userService.DeleteUser(id);
            return Ok(deletedUser);
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateUser([FromBody] UserRequest userRequest)
        {
            var user = await userService.AuthenticateUser(userRequest);
            if(user == null)
            {
                return BadRequest("Invalid email or password");
            }
            return Ok(user);
        }
    }
}
