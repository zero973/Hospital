using Ardalis.Result;
using Hospital.Core.Interfaces;
using Hospital.Core.Models.DTO;
using Hospital.Core.Models.Entities;
using MediatR;

namespace Hospital.Core.Queries.Contacts.Handlers;

internal class GetContactRequestHandler(IRepository<Contact> contactsRepository,
    IReadRepository<Patient> patientsRepository, IReadRepository<Disease> diseasesRepository)
    : IRequestHandler<GetContactRequest, Result<ContactDto>>
{
    public async Task<Result<ContactDto>> Handle(GetContactRequest request, CancellationToken cancellationToken)
    {
        var contact = await contactsRepository.GetByIdAsync(request.Id, cancellationToken);

        if (contact == null)
            return Result.NotFound("Обращение с таким Id не найдено");

        var patient = await patientsRepository.GetByIdAsync(contact.PatientId, cancellationToken);
        var disease = await diseasesRepository.GetByIdAsync(contact.DiseaseId, cancellationToken);

        return new ContactDto(contact.Id, patient!, disease!, contact.Date, contact.ResourcesSpent.ToList());
    }
}