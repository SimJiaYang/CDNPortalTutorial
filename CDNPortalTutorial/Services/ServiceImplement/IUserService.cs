using CDNPortalTutorial.Model.Dto;
using CDNPortalTutorial.Model.Entities;

namespace CDNPortalTutorial.Services.ServiceImplement
{
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync();
        Task<User> GetEmployeeAsync(Guid id);
        Task<User> CreateUserAsync(AddUserDto data);
        Task<User> UpdateUserAsync(Guid id, UpdateUserDto data);
        Task DeleteUserAsync(Guid id);
    }
}
