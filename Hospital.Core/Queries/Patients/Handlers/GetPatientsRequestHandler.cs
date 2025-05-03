using Ardalis.Result;
using Hospital.Core.Interfaces;
using Hospital.Core.Models.Entities;
using MediatR;

namespace Hospital.Core.Queries.Patients.Handlers;

internal class GetPatientsRequestHandler(IRepository<Patient> repository) 
    : IRequestHandler<GetPatientsRequest, Result<IEnumerable<Patient>>>
{
    public async Task<Result<IEnumerable<Patient>>> Handle(GetPatientsRequest request, CancellationToken cancellationToken)
    {
        return await repository.ListAsync(cancellationToken);
    }
}