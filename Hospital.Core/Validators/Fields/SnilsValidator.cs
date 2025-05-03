using FluentValidation;
using Hospital.Core.Interfaces;
using Hospital.Core.Models.Entities;
using Hospital.Core.Specifications;

namespace Hospital.Core.Validators.Fields;

internal class SnilsValidator : AbstractValidator<string>
{
    public SnilsValidator(IRepository<Patient> repository)
    {
        RuleFor(x => x)
            .Length(11)
            .WithMessage("СНИЛС должен содержать 11 цифр");

        RuleFor(x => x)
            .Must((value, _) => value.All(char.IsDigit))
            .WithMessage("СНИЛС должен содержать только цифры");

        RuleFor(x => x)
            .MustAsync(async (value, token) 
                => !await repository.AnyAsync(new PatientsSpecification(value), token))
            .WithMessage("Такой СНИЛС уже есть в базе данных");
    }
}