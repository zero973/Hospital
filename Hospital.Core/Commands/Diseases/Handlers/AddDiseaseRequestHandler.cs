using Ardalis.Result;
using Hospital.Core.Interfaces;
using Hospital.Core.Models.Entities;
using MediatR;

namespace Hospital.Core.Commands.Diseases.Handlers;

internal class AddDiseaseRequestHandler(IRepository<Disease> repository)
    : IRequestHandler<AddDiseaseRequest, Result<Disease>>
{
    public async Task<Result<Disease>> Handle(AddDiseaseRequest request, CancellationToken cancellationToken)
    {
        var result = await repository.AddAsync(
            new Disease(request.Name, request.PeriodType), 
            cancellationToken);
        return result;
    }
}