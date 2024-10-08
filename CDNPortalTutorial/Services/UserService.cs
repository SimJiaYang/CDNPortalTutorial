using CDNPortalTutorial.Data;
using CDNPortalTutorial.Exceptions;
using CDNPortalTutorial.Features.Users.Commands.CreateUser;
using CDNPortalTutorial.Features.Users.Queries.GetUserById;
using CDNPortalTutorial.Model.Dto;
using CDNPortalTutorial.Model.Entities;
using CDNPortalTutorial.Services.ServiceImplement;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CDNPortalTutorial.Services
{
    public class UserService : IUserService
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<UserService> _logger;
        private readonly ISender _sender;

        public UserService(ApplicationDbContext dbContext, ILogger<UserService> logger, ISender sender)
        {
            this._dbContext = dbContext;
            this._logger = logger;
            _sender = sender;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            _logger.LogInformation("Get Users List");
            try
            {
                List<User> users = await _dbContext.Users.ToListAsync();
                return users;
            }
            catch (Exception ex)
            {
                throw new BaseException("Error retrieving data from the database: " + ex);
            }
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            var user = await _sender.Send(new GetUserByIdQuery(id));
            return user;
        }

        public async Task<User> CreateUserAsync(CreateUserCommand command)
        {
            var user = await _sender.Send(command);
            return user;
        }

        public async Task<User> UpdateUserAsync(Guid id, UpdateUserDto data)
        {
            _logger.LogInformation("Update User Information");
            try
            {
                // Find the user by ID asynchronously
                var user = await _dbContext.Users.FindAsync(id);

                // Handle case where the user is not found
                if (user == null)
                {
                    throw new UserNotFoundException(id);
                }

                // Update the user's properties with new data
                user.Name = data.Name ?? "User";
                user.UserName = data.UserName;
                user.Email = data.Email;
                user.PhoneNumber = data.PhoneNumber;
                user.Skillsets = data.Skillsets;
                user.Hobby = data.Hobby;

                // Save the changes to the database
                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();

                return user;
            }
            catch (KeyNotFoundException ex)
            {
                throw new InvalidOperationException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error updating the user.", ex);
            }
        }

        public async Task DeleteUserAsync(Guid id)
        {
            _logger.LogInformation("Delete User");
            try
            {
                // Find the user by ID
                var user = await _dbContext.Users.FindAsync(id);

                // Check if the user was found
                if (user == null)
                {
                    throw new UserNotFoundException(id);
                }

                // Remove the user
                _dbContext.Users.Remove(user);

                // Save changes asynchronously
                await _dbContext.SaveChangesAsync();
            }
            catch (KeyNotFoundException ex)
            {
                // Log the specific not-found error (if necessary) and rethrow
                throw new InvalidOperationException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                // Log and rethrow a general error
                throw new InvalidOperationException("Error occurred while deleting the user.", ex);
            }
        }
    }
}
