using CDNPortalTutorial.Features.Users.Commands.CreateUser;
using CDNPortalTutorial.Model.Dto;
using FluentValidation;

namespace CDNPortalTutorial.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
        }
    }
}
