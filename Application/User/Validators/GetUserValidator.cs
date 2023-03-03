using Application.User.CommandHandlers;
using FluentValidation;

namespace Application.User.Validators
{
    public class GetUserValidator : AbstractValidator<GetUserCommand>
    {
        public GetUserValidator() {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id Required!!");
        }
    }
}
