using FluentValidation;
using Hospital.Core.Commands.Diseases;

namespace Hospital.Core.Validators.Requests.Diseases;

internal class AddDiseaseRequestValidator : AbstractValidator<AddDiseaseRequest>
{
    public AddDiseaseRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Наименование болезни не должно быть пустым");
    }
}