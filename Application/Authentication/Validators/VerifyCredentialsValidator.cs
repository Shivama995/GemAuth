using Application.Authentication.CommandHandlers;
using Common.Extensions;
using FluentValidation;

namespace Application.Authentication.Validators
{
    public class VerifyCredentialsValidator : AbstractValidator<VerifyCredentialsCommand>
    {
        public VerifyCredentialsValidator()
        {
            RuleFor(x => x.EmailAddress).NotEmpty().WithMessage("Email Address Required!").ValidateEmail();
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password Required!");
        }
    }
}
