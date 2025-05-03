using Ardalis.Result;
using Hospital.Core.Interfaces;
using Hospital.Core.Models.Entities;
using MediatR;

namespace Hospital.Core.Commands.Diseases.Handlers;

internal class EditDiseaseRequestHandler(IRepository<Disease> repository)
    : IRequestHandler<EditDiseaseRequest, Result<Disease>>
{
    public async Task<Result<Disease>> Handle(EditDiseaseRequest request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null)
            return Result.NotFound("Элемент с таким Id не найден");

        entity.Edit(request.Name, request.PeriodType);

        await repository.UpdateAsync(entity, cancellationToken);

        return entity;
    }
}