using FluentValidation;
using Hospital.Core.Interfaces;
using Hospital.Core.Models.DTO;
using Hospital.Core.Models.Entities;

namespace Hospital.Core.Validators.Models;

internal class ContactDtoValidator : AbstractValidator<ContactDto>
{
    public ContactDtoValidator(IRepository<Patient> repository)
    {
        RuleFor(x => x.Patient)
            .NotNull()
            .SetValidator(new PatientValidator(repository));

        RuleFor(x => x.Disease)
            .NotNull()
            .SetValidator(new DiseaseValidator());

        RuleForEach(x => x.ResourcesSpent)
            .SetValidator(new ResourceSpentValidator());
    }
}