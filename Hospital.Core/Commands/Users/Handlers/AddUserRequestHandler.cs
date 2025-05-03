using Ardalis.Result;
using Hospital.Core.Helpers;
using Hospital.Core.Interfaces;
using Hospital.Core.Models.Entities;
using MediatR;

namespace Hospital.Core.Commands.Users.Handlers;

internal class AddUserRequestHandler(IRepository<User> usersRepository) : IRequestHandler<AddUserRequest, Result<User>>
{
    public async Task<Result<User>> Handle(AddUserRequest request, CancellationToken cancellationToken)
    {
        var user = await usersRepository.AddAsync(
            new User(request.Login, Encryptor.EncryptString(request.Password), request.Role), 
            cancellationToken);
        return user;
    }
}