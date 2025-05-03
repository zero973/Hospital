using Ardalis.Result;
using Hospital.Core.Interfaces;
using Hospital.Core.Models.Entities;
using MediatR;

namespace Hospital.Core.Commands.Diseases.Handlers;

internal class DeleteDiseaseRequestHandler(IRepository<Disease> repository)
    : IRequestHandler<DeleteDiseaseRequest, Result>
{
    public async Task<Result> Handle(DeleteDiseaseRequest request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null)
            return Result.NotFound("Элемент с таким Id не найден");

        await repository.DeleteAsync(entity, cancellationToken);

        return Result.Success();
    }
}