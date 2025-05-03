using FluentValidation;
using Hospital.Core.Commands.Patients;
using Hospital.Core.Interfaces;
using Hospital.Core.Models.Entities;
using Hospital.Core.Validators.Fields;

namespace Hospital.Core.Validators.Requests.Patients;

internal class EditPatientRequestValidator : AbstractValidator<EditPatientRequest>
{
    public EditPatientRequestValidator(IRepository<Patient> repository)
    {
        RuleFor(x => x.FIO)
            .SetValidator(new FIOValidator());

        RuleFor(x => x.Birthday)
            .SetValidator(new BirthdayValidator());

        RuleFor(x => x.Snils)
            .SetValidator(new SnilsValidator(repository));
    }
}