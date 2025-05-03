using FluentValidation;
using Hospital.Core.Models.Entities;

namespace Hospital.Core.Validators.Models;

internal class DiseaseValidator : AbstractValidator<Disease>
{
    public DiseaseValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Наименование болезни не должно быть пустым");
    }
}