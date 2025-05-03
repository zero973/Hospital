using Ardalis.Result;
using Hospital.Core.Interfaces;
using Hospital.Core.Models.Entities;
using MediatR;

namespace Hospital.Core.Queries.Diseases.Handlers;

internal class GetDiseaseRequestHandler(IRepository<Disease> repository)
    : IRequestHandler<GetDiseaseRequest, Result<Disease>>
{
    public async Task<Result<Disease>> Handle(GetDiseaseRequest request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity == null)
            return Result.NotFound("Не найден элемент с таким Id");

        return entity;
    }
}