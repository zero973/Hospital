using FluentValidation;
using Hospital.Core.Queries.Users;
using Hospital.Core.Validators.Fields;

namespace Hospital.Core.Validators.Requests.Users;

internal class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Login).SetValidator(new LoginValidator());

        RuleFor(x => x.Password)
            .MinimumLength(1)
            .WithMessage("Пароль должен быть больше 1 символа");
    }
}