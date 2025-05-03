using Ardalis.Result;
using Hospital.Core.Models.Entities;
using MediatR;

namespace Hospital.Core.Queries.ResourcesSpent;

public sealed record GetResourcesSpentRequest(Guid ContactId) : IRequest<Result<IEnumerable<ResourceSpent>>>;