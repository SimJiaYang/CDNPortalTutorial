using CDNPortalTutorial.Data;
using CDNPortalTutorial.Model.Dto;
using CDNPortalTutorial.Model.Entities;
using CDNPortalTutorial.Services;
using Microsoft.AspNetCore.Mvc;


namespace CDNPortalTutorial.Controllers
{
    // localhost:xxxx/api/v1/User
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetEmployee(Guid id)
        {
            var user = await _userService.GetUserAsync(id);

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(AddUserDto data)
        {
           var createdUser = await _userService.CreateUserAsync(data);
           return Ok(createdUser);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, UpdateUserDto data)
        {
            var user = await _userService.UpdateUserAsync(id, data);
            return Ok(user);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteUser(Guid id) {
            await _userService.DeleteUserAsync(id);
            return Ok("User deleted");
        }
    }
}
