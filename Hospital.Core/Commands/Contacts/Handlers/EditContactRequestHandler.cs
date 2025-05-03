using Ardalis.Result;
using Hospital.Core.Interfaces;
using Hospital.Core.Models.DTO;
using Hospital.Core.Models.Entities;
using Hospital.Core.Queries.Contacts;
using MediatR;

namespace Hospital.Core.Commands.Contacts.Handlers;

internal class EditContactRequestHandler(ISender sender, IRepository<Contact> contactsRepository)
    : IRequestHandler<EditContactRequest, Result<ContactDto>>
{
    public async Task<Result<ContactDto>> Handle(EditContactRequest request, CancellationToken cancellationToken)
    {
        var contact = await contactsRepository.GetByIdAsync(request.Id, cancellationToken);
        if (contact == null)
            return Result.NotFound("Обращение с таким Id не найдено");

        contact.Edit(request.PatientId, request.DiseaseId, request.Date);

        await contactsRepository.UpdateAsync(contact, cancellationToken);

        return await sender.Send(new GetContactRequest(contact.Id), cancellationToken);
    }
}