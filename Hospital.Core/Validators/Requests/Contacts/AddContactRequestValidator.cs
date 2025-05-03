using FluentValidation;
using Hospital.Core.Commands.Contacts;
using Hospital.Core.Interfaces;
using Hospital.Core.Models.Entities;
using Hospital.Core.Validators.Models;

namespace Hospital.Core.Validators.Requests.Contacts;

internal class AddContactRequestValidator : AbstractValidator<AddContactRequest>
{
    public AddContactRequestValidator(IRepository<Patient> repository)
    {
        RuleForEach(x => x.ResourcesSpent)
            .SetValidator(new ResourceSpentValidator());
    }
}