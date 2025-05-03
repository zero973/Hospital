using FluentValidation;
using Hospital.Core.Commands.Diseases;

namespace Hospital.Core.Validators.Requests.Diseases;

internal class EditDiseaseRequestValidator : AbstractValidator<EditDiseaseRequest>
{
    public EditDiseaseRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Наименование болезни не должно быть пустым");
    }
}