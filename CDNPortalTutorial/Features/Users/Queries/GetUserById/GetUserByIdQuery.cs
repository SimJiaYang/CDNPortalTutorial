using CDNPortalTutorial.Model.Entities;
using MediatR;

namespace CDNPortalTutorial.Features.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(Guid Id): IRequest<User>;
}
