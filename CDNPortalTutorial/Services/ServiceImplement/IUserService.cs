using CDNPortalTutorial.Features.Users.Commands.CreateUser;
using CDNPortalTutorial.Model.Dto;
using CDNPortalTutorial.Model.Entities;

namespace CDNPortalTutorial.Services.ServiceImplement
{
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserAsync(Guid id);
        Task<User> CreateUserAsync(CreateUserCommand command);
        Task<User> UpdateUserAsync(Guid id, UpdateUserDto data);
        Task DeleteUserAsync(Guid id);
    }
}
