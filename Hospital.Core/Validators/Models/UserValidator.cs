using FluentValidation;
using Hospital.Core.Models.Entities;
using Hospital.Core.Validators.Fields;

namespace Hospital.Core.Validators.Models;

internal class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Login)
            .SetValidator(new LoginValidator());

        RuleFor(x => x.PasswordHash)
            .MinimumLength(1)
            .WithMessage("Пароль должен быть больше 1 символа");
    }
}