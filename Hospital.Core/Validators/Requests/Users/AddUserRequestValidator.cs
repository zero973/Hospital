using FluentValidation;
using Hospital.Core.Commands.Users;
using Hospital.Core.Validators.Fields;

namespace Hospital.Core.Validators.Requests.Users;

internal class AddUserRequestValidator : AbstractValidator<AddUserRequest>
{
    public AddUserRequestValidator()
    {
        RuleFor(x => x.Login)
            .SetValidator(new LoginValidator());

        RuleFor(x => x.Password)
            .MinimumLength(1)
            .WithMessage("Пароль должен быть больше 1 символа");
    }
}