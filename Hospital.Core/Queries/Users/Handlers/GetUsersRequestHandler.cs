using Ardalis.Result;
using Hospital.Core.Interfaces;
using Hospital.Core.Models.Entities;
using MediatR;

namespace Hospital.Core.Queries.Users.Handlers;

internal class GetUsersRequestHandler(IRepository<User> usersRepository) 
    : IRequestHandler<GetUsersRequest, Result<IEnumerable<User>>>
{
    public async Task<Result<IEnumerable<User>>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {
        return await usersRepository.ListAsync(cancellationToken);
    }
}