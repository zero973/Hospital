using Ardalis.Result;
using Hospital.Core.Helpers;
using Hospital.Core.Interfaces;
using Hospital.Core.Models.Entities;
using Hospital.Core.Specifications;
using MediatR;

namespace Hospital.Core.Queries.Users.Handlers;

internal class LoginRequestHandler(IRepository<User> usersRepository, IUserStore store) : IRequestHandler<LoginRequest, Result>
{
    public async Task<Result> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await usersRepository.FirstOrDefaultAsync(
            new UsersSpecification(request.Login, Encryptor.EncryptString(request.Password)), cancellationToken);

        if (user == null)
            return Result.NotFound("Пользователь с таким логином и паролем не найден");

        store.SetUser(user);

        return Result.Success();
    }
}