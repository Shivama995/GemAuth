using Application.User.CommandHandlers;
using Common.Extensions;
using FluentValidation;

namespace Application.User.Validators
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserValidator()
        {
            RuleFor(x => x.EmailAddress).NotEmpty().WithMessage("Email Address  For User Required!").ValidateEmail();
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name For User Required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password  For User Required");
            RuleFor(x => x.Role).NotEmpty().WithMessage("Role For User Required");
        }
    }
}
