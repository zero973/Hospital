using Ardalis.Result;
using Hospital.Core.Interfaces;
using Hospital.Core.Models.Entities;
using Hospital.Core.Specifications;
using MediatR;

namespace Hospital.Core.Commands.ResourcesSpent.Handlers;

internal class AddResourceSpentRequestHandler(IRepository<Contact> contactsRepository)
    : IRequestHandler<AddResourceSpentRequest, Result<ResourceSpent>>
{
    public async Task<Result<ResourceSpent>> Handle(AddResourceSpentRequest request, 
        CancellationToken cancellationToken)
    {
        var contact = await contactsRepository.FirstOrDefaultAsync(
            new ContactsSpecification(request.ContactId) , cancellationToken);
        if (contact == null)
            return Result.NotFound("Обращение с таким Id не найдено");

        var resourceSpent = new ResourceSpent(request.Resource, 
            request.Comment, request.Count);
        var addResult = contact.AddResourceSpent(resourceSpent);
        if (!addResult)
            return Result.NotFound("Ресурс с таким названием уже есть. Добавьте другой");

        await contactsRepository.UpdateAsync(contact, cancellationToken);

        return resourceSpent;
    }
}