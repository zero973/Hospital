using Ardalis.Result;
using Hospital.Core.Enums;
using Hospital.Core.Models.Entities;
using MediatR;

namespace Hospital.Core.Commands.Users;

public sealed record AddUserRequest(string Login, string Password, UserRoles Role) : IRequest<Result<User>>;

public sealed record EditUserRequest(Guid Id, string Login, string Password, UserRoles Role) : IRequest<Result<User>>;

public sealed record DeleteUserRequest(Guid Id) : IRequest<Result>;