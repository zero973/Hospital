using Ardalis.Result;
using Hospital.Core.Interfaces;
using Hospital.Core.Models.Entities;
using MediatR;

namespace Hospital.Core.Queries.Users.Handlers;

internal class GetUserRequestHandler(IRepository<User> usersRepository) : IRequestHandler<GetUserRequest, Result<User>>
{
    public async Task<Result<User>> Handle(GetUserRequest request, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetByIdAsync(request.Id, cancellationToken);

        if (user == null)
            return Result.NotFound("Не найден пользователь с таким Id");

        return user;
    }
}