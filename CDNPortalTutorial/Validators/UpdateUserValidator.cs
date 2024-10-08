using CDNPortalTutorial.Model.Dto;
using FluentValidation;

namespace CDNPortalTutorial.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            //RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("PhoneNumber is required");
        }
    }
}
