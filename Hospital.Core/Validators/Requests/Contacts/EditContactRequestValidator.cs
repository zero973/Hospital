using FluentValidation;
using Hospital.Core.Commands.Contacts;
using Hospital.Core.Interfaces;
using Hospital.Core.Models.Entities;

namespace Hospital.Core.Validators.Requests.Contacts;

internal class EditContactRequestValidator : AbstractValidator<EditContactRequest>
{
    public EditContactRequestValidator(IRepository<Patient> repository)
    {
        
    }
}