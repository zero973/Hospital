using Ardalis.Result;
using Hospital.Core.Interfaces;
using Hospital.Core.Models.Entities;
using MediatR;

namespace Hospital.Core.Queries.Patients.Handlers;

internal class GetPatientRequestHandler(IRepository<Patient> repository) 
    : IRequestHandler<GetPatientRequest, Result<Patient>>
{
    public async Task<Result<Patient>> Handle(GetPatientRequest request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null)
            return Result.NotFound("Не найден элемент с таким Id");
        return entity;
    }
}
