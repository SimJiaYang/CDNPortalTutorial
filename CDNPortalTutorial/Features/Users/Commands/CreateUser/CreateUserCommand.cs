using CDNPortalTutorial.Model.Entities;
using MediatR;

namespace CDNPortalTutorial.Features.Users.Commands.CreateUser
{
    public record CreateUserCommand(string Name, string UserName,
        string Email, string PhoneNumber, List<string> Skillsets, 
        List<string> Hobby
        ) : IRequest<User>;
}
