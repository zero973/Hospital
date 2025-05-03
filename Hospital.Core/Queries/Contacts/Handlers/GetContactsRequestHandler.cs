using Ardalis.Result;
using Hospital.Core.Interfaces;
using Hospital.Core.Models.DTO;
using Hospital.Core.Models.Entities;
using Hospital.Core.Specifications;
using MediatR;

namespace Hospital.Core.Queries.Contacts.Handlers;

internal class GetContactsRequestHandler(IRepository<Contact> contactsRepository, 
    IReadRepository<Patient> patientsRepository, IReadRepository<Disease> diseasesRepository)
    : IRequestHandler<GetContactsRequest, Result<IEnumerable<ContactDto>>>
{
    public async Task<Result<IEnumerable<ContactDto>>> Handle(GetContactsRequest request, CancellationToken cancellationToken)
    {
        var contacts = await contactsRepository.ListAsync(cancellationToken);

        var patientIds = contacts.Select(c => c.PatientId).Distinct().ToArray();
        var diseaseIds = contacts.Select(c => c.DiseaseId).Distinct().ToArray();

        var patients = await patientsRepository.ListAsync(new PatientsSpecification(patientIds), cancellationToken);
        var diseases = await diseasesRepository.ListAsync(new DiseasesSpecification(diseaseIds), cancellationToken);

        var patientDict = patients.ToDictionary(p => p.Id);
        var diseaseDict = diseases.ToDictionary(d => d.Id);

        var result = contacts.Select(contact =>
        {
            var patient = patientDict[contact.PatientId];
            var disease = diseaseDict[contact.DiseaseId];

            return new ContactDto(contact.Id, patient, disease, contact.Date, contact.ResourcesSpent.ToList());
        });

        return Result.Success(result);
    }
}