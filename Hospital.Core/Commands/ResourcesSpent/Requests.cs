using Ardalis.Result;
using Hospital.Core.Models.Entities;
using MediatR;

namespace Hospital.Core.Commands.ResourcesSpent;

public sealed record AddResourceSpentRequest(Guid ContactId, string Resource, string Comment, uint Count) 
    : IRequest<Result<ResourceSpent>>;

public sealed record EditResourceSpentRequest(Guid ContactId, string Resource, string Comment, uint Count) 
    : IRequest<Result<ResourceSpent>>;

public sealed record DeleteResourceSpentRequest(Guid ContactId, string Resource) : IRequest<Result>;