using Application.Org.CommandHandlers;
using Common.Extensions;
using FluentValidation;

namespace Application.Org.Validators
{
    public class RegisterAppValidator : AbstractValidator<RegisterAppCommand>
    {
        public RegisterAppValidator()
        {
            RuleFor(x => x.EmailAddress).NotEmpty().WithMessage("Email Address  For User Required!").ValidateEmail();
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name For User Required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password  For User Required");
            RuleFor(x => x.OrgName).NotEmpty().WithMessage("Org Name Required");
        }
    }
}
