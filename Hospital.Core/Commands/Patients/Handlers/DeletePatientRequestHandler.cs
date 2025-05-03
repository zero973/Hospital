using Ardalis.Result;
using Hospital.Core.Interfaces;
using Hospital.Core.Models.Entities;
using MediatR;

namespace Hospital.Core.Commands.Patients.Handlers;

internal class DeletePatientRequestHandler(IRepository<Patient> repository) : IRequestHandler<DeletePatientRequest, Result>
{
    public async Task<Result> Handle(DeletePatientRequest request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        await repository.DeleteAsync(entity!, cancellationToken);
        return Result.Success();
    }
}