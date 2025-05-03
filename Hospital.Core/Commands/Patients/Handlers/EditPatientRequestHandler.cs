using Ardalis.Result;
using Hospital.Core.Interfaces;
using Hospital.Core.Models.Entities;
using MediatR;

namespace Hospital.Core.Commands.Patients.Handlers;

internal class EditPatientRequestHandler(IRepository<Patient> repository) : IRequestHandler<EditPatientRequest, Result<Patient>>
{
    public async Task<Result<Patient>> Handle(EditPatientRequest request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        entity!.Edit(request.FIO, request.Birthday, request.Snils);
        await repository.UpdateAsync(entity, cancellationToken);
        return entity;
    }
}