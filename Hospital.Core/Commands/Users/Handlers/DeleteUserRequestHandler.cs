using Ardalis.Result;
using Hospital.Core.Interfaces;
using Hospital.Core.Models.Entities;
using MediatR;

namespace Hospital.Core.Commands.Users.Handlers;

internal class DeleteUserRequestHandler(IRepository<User> usersRepository) : IRequestHandler<DeleteUserRequest, Result>
{
    public async Task<Result> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetByIdAsync(request.Id, cancellationToken);

        if (user == null)
            return Result.NotFound("Пользователь с таким Id не найден");

        await usersRepository.DeleteAsync(user, cancellationToken);

        return Result.Success();
    }
}