using Ardalis.Result;
using Hospital.Core.Interfaces;
using Hospital.Core.Models.Entities;
using MediatR;

namespace Hospital.Core.Queries.Diseases.Handlers;

internal class GetDiseasesRequestHandler(IRepository<Disease> repository)
    : IRequestHandler<GetDiseasesRequest, Result<IEnumerable<Disease>>>
{
    public async Task<Result<IEnumerable<Disease>>> Handle(GetDiseasesRequest request, 
        CancellationToken cancellationToken)
    {
        return await repository.ListAsync(cancellationToken);
    }
}