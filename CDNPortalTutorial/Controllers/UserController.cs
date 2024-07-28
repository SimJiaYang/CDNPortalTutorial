using CDNPortalTutorial.Data;
using CDNPortalTutorial.Model.Dto;
using CDNPortalTutorial.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CDNPortalTutorial.Controllers
{
    // localhost:xxxx/api/User
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public UserController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
                var users = dbContext.Users.ToList();
                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployee(Guid id)
        {
            try
            {
                var user = dbContext.Users.Find(id);

                if (user is null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public IActionResult CreateUser(AddUserDto data)
        {
            try
            {
                if (data == null)
                {
                    return BadRequest();
                }

                var user = new User
                {
                    Name = data.Name,
                    UserName = data.UserName,
                    Email = data.Email,
                    PhoneNumber = data.PhoneNumber,
                    Skillsets = data.Skillsets,
                    Hobby = data.Hobby
                };

                var email_exist = dbContext.Users.Any(x => x.Email == user.Email);

               if (email_exist == true)
                {
                    ModelState.AddModelError("email", "Employee email already in use");
                    return BadRequest(ModelState);
                }

                var createdUser = dbContext.Users.Add(user);
                dbContext.SaveChanges();

                return Ok(createdUser.Entity);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating employee");
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateUser(Guid id, UpdateUserDto data)
        {
            try
            {
                var user = dbContext.Users.Find(id);

                if (user is null)
                {
                    return NotFound("User not found");
                }

                // Update User
                user.Name = data.Name;
                user.UserName = data.UserName;
                user.Email = data.Email;
                user.PhoneNumber = data.PhoneNumber;
                user.Skillsets = data.Skillsets;
                user.Hobby = data.Hobby;

                dbContext.SaveChanges();

                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating employee");
            }
        }

        [HttpDelete]
        public IActionResult DeleteUser(Guid id) {
            try
            {
                var user = dbContext.Users.Find(id);

                if (user is null)
                {
                    return NotFound("User not found");
                }

                dbContext.Users.Remove(user);
                dbContext.SaveChanges();

                return Ok("User deleted");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting employee");
            }
        }
    }
}
