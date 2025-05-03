using Ardalis.Result;
using Hospital.Core.Interfaces;
using Hospital.Core.Models.Entities;
using MediatR;

namespace Hospital.Core.Commands.ResourcesSpent.Handlers;

internal class DeleteResourceSpentRequestHandler(IRepository<Contact> contactsRepository)
    : IRequestHandler<DeleteResourceSpentRequest, Result>
{
    public async Task<Result> Handle(DeleteResourceSpentRequest request,
        CancellationToken cancellationToken)
    {
        var contact = await contactsRepository.GetByIdAsync(
            request.ContactId, cancellationToken);
        if (contact == null)
            return Result.NotFound("Обращение с таким Id не найдено");

        var oldResourcesSpent = contact.ResourcesSpent
            .SingleOrDefault(x => x.Resource == request.Resource);
        if (oldResourcesSpent == null)
            return Result.NotFound("Ресурс с таким названием не найден");

        contact.RemoveResourceSpent(oldResourcesSpent);

        await contactsRepository.UpdateAsync(contact, cancellationToken);

        return Result.Success();
    }
}