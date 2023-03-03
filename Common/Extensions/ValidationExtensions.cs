using FluentValidation;

namespace Common.Extensions
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> ValidateEmail<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Matches("^([a-zA-Z0-9_\\.\\-\\+])+\\@(([a-zA-Z0-9\\-])+\\.)+([a-zA-Z0-9]{2,4})+$").WithMessage("Invalid Email Address");
        }
    }
}
