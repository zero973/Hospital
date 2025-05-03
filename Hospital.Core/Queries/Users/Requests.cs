using Ardalis.Result;
using Hospital.Core.Models.Entities;
using MediatR;

namespace Hospital.Core.Queries.Users;

public sealed record GetUserRequest(Guid Id) : IRequest<Result<User>>;

public sealed record GetUsersRequest() : IRequest<Result<IEnumerable<User>>>;

public sealed record LoginRequest(string Login, string Password) : IRequest<Result>;