using FluentValidation;

namespace Hospital.Core.Validators.Fields;

internal class BirthdayValidator : AbstractValidator<DateOnly>
{
    public BirthdayValidator()
    {
        RuleFor(x => x)
            .LessThan(DateOnly.FromDateTime(DateTime.Now))
            .WithMessage("День рождения должен быть меньше текущей даты");
    }
}