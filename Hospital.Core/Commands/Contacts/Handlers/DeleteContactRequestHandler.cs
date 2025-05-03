using Ardalis.Result;
using Hospital.Core.Interfaces;
using Hospital.Core.Models.Entities;
using MediatR;

namespace Hospital.Core.Commands.Contacts.Handlers;

internal class DeleteContactRequestHandler(IRepository<Contact> contactsRepository)
    : IRequestHandler<DeleteContactRequest, Result>
{
    public async Task<Result> Handle(DeleteContactRequest request, CancellationToken cancellationToken)
    {
        var contact = await contactsRepository.GetByIdAsync(request.Id, cancellationToken);
        if (contact == null)
            return Result.NotFound("Обращение с таким Id не найдено");

        await contactsRepository.DeleteAsync(contact, cancellationToken);

        return Result.Success();
    }
}