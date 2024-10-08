using CDNPortalTutorial.Data;
using CDNPortalTutorial.Exceptions;
using CDNPortalTutorial.Features.Users.Commands.CreateUser;
using CDNPortalTutorial.Model.Entities;
using CDNPortalTutorial.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CDNPortalTutorial.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<UserService> _logger;

        public GetUserByIdQueryHandler(ApplicationDbContext dbContext, ILogger<UserService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var id = request.Id;

            _logger.LogInformation("Get User Information by ID");
            try
            {
                var user = await _dbContext.Users.FindAsync(id);

                if (user == null)
                {
                    throw new UserNotFoundException(id);
                }

                return user;
            }
            catch (KeyNotFoundException ex)
            {
                throw new InvalidOperationException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BaseException("Error retrieving data from the database: " + ex);
            }
        }
    }
}
