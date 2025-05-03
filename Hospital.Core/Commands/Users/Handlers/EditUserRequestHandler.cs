using Ardalis.Result;
using Hospital.Core.Helpers;
using Hospital.Core.Interfaces;
using Hospital.Core.Models.Entities;
using MediatR;

namespace Hospital.Core.Commands.Users.Handlers;

internal class EditUserRequestHandler(IRepository<User> usersRepository) : IRequestHandler<EditUserRequest, Result<User>>
{
    public async Task<Result<User>> Handle(EditUserRequest request, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetByIdAsync(request.Id, cancellationToken);
        user!.Edit(request.Login, Encryptor.EncryptString(request.Password), request.Role);

        await usersRepository.UpdateAsync(user, cancellationToken);

        return user;
    }
}