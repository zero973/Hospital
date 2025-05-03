using FluentValidation;

namespace Hospital.Core.Validators.Fields;

internal class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
    {
        RuleFor(x => x)
            .MinimumLength(1)
            .WithMessage("Пароль должен быть больше 1 символа");
    }
}