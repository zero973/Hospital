using Ardalis.Result;
using Hospital.Core.Interfaces;
using Hospital.Core.Models.DTO;
using Hospital.Core.Models.Entities;
using Hospital.Core.Queries.Contacts;
using MediatR;

namespace Hospital.Core.Commands.Contacts.Handlers;

internal class AddContactRequestHandler(ISender sender, IRepository<Contact> contactsRepository)
    : IRequestHandler<AddContactRequest, Result<ContactDto>>
{
    public async Task<Result<ContactDto>> Handle(AddContactRequest request, CancellationToken cancellationToken)
    {
        var contact = new Contact(
            request.PatientId,
            request.DiseaseId,
            request.Date,
            request.ResourcesSpent);

        await contactsRepository.AddAsync(contact, cancellationToken);

        return await sender.Send(new GetContactRequest(contact.Id), cancellationToken);
    }
}