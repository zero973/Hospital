using FluentValidation;

namespace Hospital.Core.Validators.Fields;

internal class LoginValidator : AbstractValidator<string>
{
    public LoginValidator()
    {
        RuleFor(x => x)
            .MinimumLength(1)
            .WithMessage("Логин должен быть больше 1 символа");
    }
}