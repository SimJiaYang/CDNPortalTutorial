using CDNPortalTutorial.Data;
using CDNPortalTutorial.Model.Entities;
using CDNPortalTutorial.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CDNPortalTutorial.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<UserService> _logger;

        public CreateUserCommandHandler(ApplicationDbContext dbContext, ILogger<UserService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Validate User");

            var data = new User { Email = request.Email, 
                Name = request.Name, PhoneNumber = request.PhoneNumber, 
                Skillsets = request.Skillsets, Hobby = request.Hobby, 
                UserName = request.UserName };

            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), "User data cannot be null");
            }

            // Check if the email already exists
            var emailExists = await _dbContext.Users.AnyAsync(x => x.Email == data.Email);
            if (emailExists)
            {
                throw new InvalidOperationException("Email already exists. Please use a different email.");
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

            _logger.LogInformation("Create User");
            try
            {
                var createdUser = await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();

                return createdUser.Entity;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error occurred while creating the user.", ex);
            }
        }
    }
}
