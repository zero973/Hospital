using Ardalis.Result;
using Hospital.Core.Interfaces;
using Hospital.Core.Models.Entities;
using MediatR;

namespace Hospital.Core.Queries.ResourcesSpent.Handlers;

internal class GetResourcesSpentRequestHandler(IRepository<Contact> contactsRepository)
    : IRequestHandler<GetResourcesSpentRequest, Result<IEnumerable<ResourceSpent>>>
{
    public async Task<Result<IEnumerable<ResourceSpent>>> Handle(GetResourcesSpentRequest request, 
        CancellationToken cancellationToken)
    {
        var entity = await contactsRepository.GetByIdAsync(request.ContactId, cancellationToken);

        if (entity == null)
            return Result.NotFound("Обращение с таким Id не найдено");

        return Result.Success(entity.ResourcesSpent.AsEnumerable());
    }
}