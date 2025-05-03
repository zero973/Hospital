using Ardalis.Result;
using Hospital.Core.Interfaces;
using Hospital.Core.Models.Entities;
using MediatR;

namespace Hospital.Core.Commands.Patients.Handlers;

internal class AddPatientRequestHandler(IRepository<Patient> repository) : IRequestHandler<AddPatientRequest, Result<Patient>>
{
    public async Task<Result<Patient>> Handle(AddPatientRequest request, CancellationToken cancellationToken)
    {
        var entity = await repository.AddAsync(
            new Patient(request.FIO, request.Birthday, request.Snils), 
            cancellationToken);
        return entity;
    }
}