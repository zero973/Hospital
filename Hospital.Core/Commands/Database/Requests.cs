using Ardalis.Result;
using MediatR;

namespace Hospital.Core.Commands.Database;

public sealed record MigrateDatabase() : IRequest<Result>;